using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Builders;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Plugins.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Botted.Core
{
	/// <inheritdoc cref="Botted.Core.Abstractions.Builders.IBotBuilder" />
	public class BotBuilder : AbstractBuilder<BotBuilder, IBot>, IBotBuilder
	{
		private readonly string _botPath;
		private readonly ContainerBuilder _containerBuilder;

		/// <summary>
		/// Initializes new instance of <see cref="BotBuilder"/>
		/// </summary>
		public BotBuilder()
		{
			_botPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
			
			_containerBuilder = new ContainerBuilder();
			_containerBuilder.RegisterType<Bot>()
							 .As<IBot>();
		}

		public IBotBuilder LoadPlugins(string pluginsDirectory)
		{
			var path = Path.Combine(_botPath, pluginsDirectory);
			var assemblies = Directory.GetFiles(path, "*.dll")
									  .Select(Assembly.LoadFile);
			var pluginTypes = assemblies.SelectMany(a => a.GetTypes())
										.Where(t => t.IsAssignableTo(typeof(IPlugin)));
			var plugins = pluginTypes.Select(Activator.CreateInstance)
									 .Cast<IPlugin>()
									 .Apply(LoadPlugin)
									 .ToList();
			return this;
		}

		public IBotBuilder LoadConfigs(string configDirectory)
		{
			var path = Path.Combine(_botPath, configDirectory);
			var configurationBuilder = new ConfigurationBuilder();
			
			configurationBuilder.SetBasePath(path);
			var files = Directory.GetFiles(path, "*.json")
								 .Apply(f => configurationBuilder.AddJsonFile(f))
								 .ToList();

			_containerBuilder.RegisterInstance(configurationBuilder.Build())
							 .As<IConfiguration>()
							 .SingleInstance();

			return this;
		}


		public IBotBuilder ConfigureLogger(Action<ILoggerFactory> configure)
		{
			_containerBuilder.RegisterInstance(new LoggerFactory().Apply(configure))
							 .As<ILoggerFactory>()
							 .SingleInstance()
							 .AutoActivate();
			
			_containerBuilder.RegisterGeneric(typeof(Logger<>))
							 .As(typeof(ILogger<>))
							 .SingleInstance();

			return this;
		}

		public IBotBuilder ConfigureServices(Action<ContainerBuilder> configure)
			=> InvokeActionWithArgument(configure, _containerBuilder);

		public override IBot Build() => _containerBuilder.Build().Resolve<IBot>();

		private void LoadPlugin(IPlugin plugin)
		{
			Log.Logger.Information("Loading plugin {0}", plugin.Name);
			plugin.OnLoad(this);
			Log.Logger.Information("Plugin {0} loaded!", plugin.Name);
		}
	}
}