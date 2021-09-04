using System;
using Autofac;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Builders;
using Botted.Core.Abstractions.Extensions;
using Microsoft.Extensions.Logging;

namespace Botted.Core
{
	/// <inheritdoc cref="Botted.Core.Abstractions.Builders.IBotBuilder" />
	public class BotBuilder : AbstractBuilder<BotBuilder, IBot>, IBotBuilder
	{
		private readonly ContainerBuilder _containerBuilder;

		/// <summary>
		/// Initializes new instance of <see cref="BotBuilder"/>
		/// </summary>
		public BotBuilder()
		{
			_containerBuilder = new ContainerBuilder();
			_containerBuilder.RegisterType<Bot>()
							 .As<IBot>();
		}

		public IBotBuilder LoadPlugins(string pluginsDirectory)
		{
			return this;
		}

		public IBotBuilder LoadConfig(string configFileName)
		{
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
	}
}