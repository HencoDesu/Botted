using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Botted.Core.Abstractions;
using Botted.Core.Dependencies;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Logging;

namespace Botted.Core.Extensions
{
	public static class HostBuilderExtensions
	{
		/// <summary>
		/// Use Botted in <see cref="IHost"/> built by <see cref="IHostBuilder"/>
		/// </summary>
		/// <param name="builder">Builder to create <see cref="IHost"/></param>
		/// <param name="configureBot">Action to configure Botted</param>
		/// <returns>Current <see cref="IHostBuilder"/></returns>
		public static IHostBuilder UseBotted(this IHostBuilder builder, Action<IBottedBuilder> configureBot)
		{
			builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				   .ConfigureContainer<ContainerBuilder>((hostBuilderContext, containerBuilder) =>
				   {
					   var wrappedContainerBuilder = new AutofacContainerBuilder(containerBuilder);
					   var loggerFactory = new SerilogLoggerFactory(Log.Logger);
					   var rootDirectory = new DirectoryInfo(hostBuilderContext.HostingEnvironment.ContentRootPath);
					   var bottedBuilder = new BottedBuilder(wrappedContainerBuilder, loggerFactory, rootDirectory);
					   configureBot(bottedBuilder);

					   containerBuilder.RegisterType<Bot>().AsImplementedInterfaces().SingleInstance();
				   });

			return builder;
		}
	}
}