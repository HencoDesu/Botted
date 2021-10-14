using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Commands;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Configuration;
using Botted.Core.Events;
using Botted.Core.Events.Abstractions;
using Botted.Core.Users;
using Botted.Core.Users.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Botted.Core.Extensions
{
	public static class HostBuilderExtensions
	{
		public static IHostBuilder UseBotted<TBot>(this IHostBuilder builder) 
			where TBot : class, IBot
		{
			return UseBotted<TBot>(builder, _ => { });
		}

		public static IHostBuilder UseBotted<TBot>(this IHostBuilder builder, Action<BottedConfiguration> configureBot)
			where TBot : class, IBot
		{
			var configuration = new BottedConfiguration().Apply(configureBot);
			var botPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

			builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				   .ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
				   {
					   containerBuilder.RegisterType<TBot>()
									   .AsImplementedInterfaces()
									   .SingleInstance();

					   LoadLibraries(Path.Combine(botPath, configuration.LibsPath));
					   LoadPlugins(Path.Combine(botPath, configuration.PluginsPath), containerBuilder);
					   LoadConfigs(Path.Combine(botPath, configuration.ConfigPath), containerBuilder);

					   containerBuilder.RegisterService<IEventService, EventService>();
					   containerBuilder.RegisterService<IUserService, UserService>();
					   containerBuilder.RegisterService<ICommandService, CommandService>();
					   
					   containerBuilder.RegisterSingleton<ICommandParser, CommandParser>();
				   });

			return builder;
		}

		private static void LoadLibraries(string librariesPath)
		{
			CreateDirectoryIfNotExist(librariesPath);
			
			Directory.GetFiles(librariesPath, "*.dll")
					 .ApplyToEachImmediately(lib => Assembly.LoadFrom(lib));
		}

		private static void LoadPlugins(string pluginsPath, ContainerBuilder containerBuilder)
		{
			CreateDirectoryIfNotExist(pluginsPath);

			var plugins = Directory.GetFiles(pluginsPath, "*.dll")
								   .Select(Assembly.LoadFrom)
								   .SelectMany(a => a.GetExportedTypes())
								   .Where(t => t.IsAssignableTo(typeof(IPlugin)) && !t.IsAbstract)
								   .Select(Activator.CreateInstance)
								   .Cast<IPlugin>()
								   .ToList();
			
			plugins.ApplyToEachImmediately(plugin => plugin.OnInit(containerBuilder));
			
			containerBuilder.RegisterBuildCallback(scope =>
			{
				var logger = scope.Resolve<ILogger>();
				plugins.ApplyToEachImmediately(plugin =>
				{
					logger.LogInformation("Loading plugin {plugin.Name}", plugin.Name);
					plugin.OnLoad(scope);
					logger.LogInformation("Plugin {plugin.Name} loaded", plugin.Name);
				});
			});
		}

		private static void LoadConfigs(string configsPath, ContainerBuilder containerBuilder)
		{
			CreateDirectoryIfNotExist(configsPath);
			
			var configurationBuilder = new ConfigurationBuilder();

			configurationBuilder.SetBasePath(configsPath);
			Directory.GetFiles(configsPath, "*.json")
					 .ApplyToEachImmediately(f => configurationBuilder.AddJsonFile(f));

			var config = configurationBuilder.Build();
			
			containerBuilder.RegisterInstance(config)
							.As<IConfiguration>()
							.SingleInstance();
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