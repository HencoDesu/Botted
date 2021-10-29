using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Dependencies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
			LoadPlugins(Path.Combine(_botPath, pluginsPath));
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

		private void LoadPlugins(string pluginsPath)
		{
			CreateDirectoryIfNotExist(pluginsPath);

			var plugins = Directory.GetFiles(pluginsPath, "*.dll")
								   .Select(Assembly.LoadFrom)
								   .SelectMany(a => a.GetExportedTypes())
								   .Where(t => t.IsAssignableTo(typeof(IPlugin)) && !t.IsAbstract)
								   .Select(Activator.CreateInstance)
								   .Cast<IPlugin>()
								   .ToList();

			plugins.ApplyToEachImmediately(plugin => plugin.OnInit(this));

			_containerBuilder.RegisterBuildCallback(scope =>
			{
				var logger = scope.Resolve<ILogger<IBot>>();
				plugins.ApplyToEachImmediately(plugin =>
				{
					logger.LogInformation("Loading plugin {plugin.Name}", plugin.Name);
					plugin.OnLoad(scope);
					logger.LogInformation("Plugin {plugin.Name} loaded", plugin.Name);
				});
			});
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