using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Commands.Extensions;
using Botted.Core.Events.Extensions;
using Botted.Core.Users.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Botted.Core.Extensions
{
	public static class HostBuilderExtensions
	{
		public static IHostBuilder UseBotted<TBot>(this IHostBuilder builder) 
			where TBot : class, IBot
		{
			return builder.UseBotted<TBot>(bottedBuilder =>
			{
				bottedBuilder.UseLibrariesFolder("Libs")
							 .UsePluginsFolder("Plugins")
							 .UseConfigsFolder("Configuration")
							 .UseDefaultEventService()
							 .UseDefaultCommandService()
							 .UseDefaultCommandParser()
							 .UseDefaultUserService()
							 .UseDefaultUserDatabase();
			});
		}

		public static IHostBuilder UseBotted<TBot>(this IHostBuilder builder, Action<IBottedBuilder> configureBot)
			where TBot : class, IBot
		{
			builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				   .ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
				   {
					   var bottedBuilder = new BottedBuilder<TBot>(containerBuilder).Apply(configureBot);

					   containerBuilder.RegisterInstance(bottedBuilder)
									   .As<IBottedBuilder>()
									   .SingleInstance();

					   containerBuilder.RegisterType<LoggerFactory>()
									   .As<ILoggerFactory>()
									   .SingleInstance();
					   containerBuilder.RegisterGeneric(typeof(Logger<>))
									   .As(typeof(ILogger<>))
									   .SingleInstance();
				   });

			return builder;
		}
	}
}