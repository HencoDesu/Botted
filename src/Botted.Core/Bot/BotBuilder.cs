using System;
using System.IO;
using System.Reflection;
using Autofac;
using Botted.Core.Extensions;
using Botted.Core.Abstractions.Bot;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Events;
using NLog;

namespace Botted.Core.Bot
{
	public class BotBuilder : IBotBuilder
	{
		private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
		private readonly ContainerBuilder _builder = new();

		public BotBuilder()
		{
			_builder.RegisterType<Bot>()
					.AsSelf();
		}

		public IBotBuilder LoadPlugins()
		{
			_logger.Info("Load plugins...");

			var pluginsPath = Path.Combine(Environment.CurrentDirectory, "Plugins");
			var allPlugins = new DirectoryInfo(pluginsPath).GetFiles();
			foreach (var plugin in allPlugins)
			{
				Assembly.LoadFile(plugin.FullName);
			}

			return this;
		}

		public IBotBuilder RegisterEvents()
		{
			_logger.Info("Register events...");
			return RegisterInheritedTypes(typeof(IEvent));
		}

		public IBotBuilder RegisterServices()
		{
			_logger.Info("Register services...");
			return RegisterInheritedTypes(typeof(IService));
		}

		public IBotBuilder RegisterCommands()
		{
			_logger.Info("Register commands...");
			return RegisterInheritedTypes(typeof(ICommand));
		}

		public IBotBuilder RegisterFactories()
		{
			_logger.Info("Register factories...");
			return RegisterInheritedTypes(typeof(IFactory));
		}

		public IBot Build()
		{
			_logger.Info("Bot intialization done!");
			var bot = _builder.Build().Resolve<Bot>();
			return bot;
		}

		private IBotBuilder RegisterInheritedTypes(Type type)
		{
			_builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
					.PublicOnly()
					.Where(t => t.InheritedFrom(type))
					.AsImplementedInterfaces()
					.SingleInstance();
			return this;
		}
	}
}