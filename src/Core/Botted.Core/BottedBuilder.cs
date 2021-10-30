using System;
using System.IO;
using System.Reflection;
using Autofac;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Dependencies;
using Botted.Core.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;
using Serilog.Extensions.Logging;

namespace Botted.Core
{
	/// <inheritdoc />
	public class BottedBuilder<TBot> : IBottedBuilder
		where TBot : notnull
	{
		private readonly IContainerBuilder _containerBuilder;
		private readonly string _botPath;

		internal BottedBuilder(ContainerBuilder containerBuilder)
		{
			_botPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

			containerBuilder.RegisterType<TBot>()
							.AsImplementedInterfaces()
							.SingleInstance();

			_containerBuilder = new AutofacContainerBuilder(containerBuilder);
		}

		public IBottedBuilder UseLibrariesFolder(string librariesPath)
		{
			LoadLibraries(Path.Combine(_botPath, librariesPath));
			return this;
		}

		public IBottedBuilder UsePluginsFolder(string pluginsPath)
		{
			var fullPath = Path.Combine(_botPath, pluginsPath);
			CreateDirectoryIfNotExist(fullPath);

			var loader = new PluginLoader(new SerilogLoggerFactory(Log.Logger).CreateLogger<PluginLoader>(), fullPath);
			var plugins = loader.LoadPlugins();
			plugins.ApplyToEachImmediately(p => p.OnInit(this));
			_containerBuilder.RegisterBuildCallback(container =>
			{
				plugins.ApplyToEachImmediately(p => p.OnLoad(container));
			});
			return this;
		}

		public IBottedBuilder UseConfigsFolder(string configsPath)
		{
			LoadConfigs(Path.Combine(_botPath, configsPath));
			return this;
		}

		public IBottedBuilder ConfigureContainer(Action<IContainerBuilder> configure)
		{
			_containerBuilder.Apply(configure);
			return this;
		}
		
		private void LoadLibraries(string librariesPath)
		{
			CreateDirectoryIfNotExist(librariesPath);

			Directory.GetFiles(librariesPath, "*.dll")
					 .ApplyToEachImmediately(lib => Assembly.LoadFrom(lib));
		}

		private void LoadConfigs(string configsPath)
		{
			CreateDirectoryIfNotExist(configsPath);

			var configurationBuilder = new ConfigurationBuilder();

			configurationBuilder.SetBasePath(configsPath);
			Directory.GetFiles(configsPath, "*.json")
					 .ApplyToEachImmediately(f => configurationBuilder.AddJsonFile(f));

			var config = configurationBuilder.Build();
			
			_containerBuilder.RegisterSingleton<IConfiguration>(config);
		}

		private static void CreateDirectoryIfNotExist(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}
		}
	}
}