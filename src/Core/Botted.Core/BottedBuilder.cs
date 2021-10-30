using System;
using System.IO;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Botted.Core
{
	/// <inheritdoc />
	public class BottedBuilder : IBottedBuilder
	{
		private readonly IContainerBuilder _containerBuilder;
		private readonly ILoggerFactory _loggerFactory;
		private readonly DirectoryInfo _botRootDirectory;

		public BottedBuilder(IContainerBuilder containerBuilder,
							 ILoggerFactory loggerFactory,
							 DirectoryInfo rootDirectory)
		{
			_containerBuilder = containerBuilder;
			_loggerFactory = loggerFactory;
			_botRootDirectory = rootDirectory;
		}
		
		public IBottedBuilder UsePluginsFolder(string pluginsPath)
		{
			var pluginDirectory = _botRootDirectory.CreateSubdirectory(pluginsPath);
			var loader = new PluginLoader(_loggerFactory.CreateLogger<PluginLoader>(), pluginDirectory);
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
			var configsDirectory = _botRootDirectory.CreateSubdirectory(configsPath);
			var configurationBuilder = new ConfigurationBuilder();

			configurationBuilder.SetBasePath(configsDirectory.FullName);
			configsDirectory.GetFiles("*.json")
							.ApplyToEachImmediately(f => configurationBuilder.AddJsonFile(f.Name));

			var config = configurationBuilder.Build();
			
			_containerBuilder.RegisterSingleton<IConfiguration>(config);
			return this;
		}

		public IBottedBuilder ConfigureServices(Action<IContainerBuilder> configure)
		{
			_containerBuilder.Apply(configure);
			return this;
		}
	}
}