using System;
using System.IO;
using System.Reflection;
using Autofac;
using Botted.Core.Extensions;
using Botted.Core.Abstractions.Bot;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Database;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Parsing;
using NLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
			_builder.RegisterInheritedTypes(typeof(IEvent));
			return this;
		}

		public IBotBuilder RegisterServices()
		{
			_logger.Info("Register services...");
			_builder.RegisterInheritedTypes(typeof(IService));
			return this;
		}

		public IBotBuilder RegisterCommands()
		{
			_logger.Info("Register commands...");
			_builder.RegisterInheritedTypes(typeof(ICommand));
			return this;
		}

		public IBotBuilder RegisterFactories()
		{
			_logger.Info("Register factories...");
			_builder.RegisterInheritedTypes(typeof(IFactory));
			return this;
		}

		public IBotBuilder RegisterConverters()
		{
			_logger.Info("Register factories...");
			_builder.RegisterInheritedTypes(typeof(IConverter<,>));
			_builder.RegisterInheritedTypes(typeof(IEnumConverter<>));
			return this;
		}

		public IBotBuilder ReadConfig()
		{
			var configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
														  .AddJsonFile("botSettings.json")
														  .Build();

			_builder.RegisterInstance(configuration)
					.As<IConfigurationRoot>()
					.SingleInstance();
			return this;
		}

		public IBotBuilder ConfigureDb<TDb>(Action<DbContextOptionsBuilder<TDb>>? builder = null)
			where TDb : DbContext, IBotDatabase
		{
			var contextBuilder = new DbContextOptionsBuilder<TDb>();
			contextBuilder.Invoke(builder);

			_builder.RegisterType<TDb>()
					.WithParameter("options", contextBuilder.Options)
					.AsSelf()
					.AsImplementedInterfaces()
					.SingleInstance();
			return this;
		}

		public IBot Build()
		{
			_logger.Info("Bot intialization done!");
			var bot = _builder.Build().Resolve<Bot>();
			return bot;
		}
	}
}