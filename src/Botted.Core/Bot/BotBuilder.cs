using System;
using System.IO;
using System.Reflection;
using Autofac;
using Botted.Core.Extensions;
using Botted.Core.Abstractions.Bot;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Database;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Services.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Botted.Core.Bot
{
	public class BotBuilder : IBotBuilder
	{
		private readonly ContainerBuilder _builder = new();

		public BotBuilder()
		{
			_builder.RegisterType<Bot>()
					.AsSelf();
		}

		public IBotBuilder LoadPlugins()
		{
			var pluginsPath = Path.Combine(Environment.CurrentDirectory, "Plugins");
			var allPlugins = new DirectoryInfo(pluginsPath).GetFiles();
			foreach (var plugin in allPlugins)
			{
				Assembly.LoadFile(plugin.FullName);
			}

			return this;
		}

		public IBotBuilder RegisterEvents()
			=> RegisterInheritedTypes(typeof(IEvent));

		public IBotBuilder RegisterServices()
			=> RegisterInheritedTypes(typeof(IService));

		public IBotBuilder RegisterCommands()
			=> RegisterInheritedTypes(typeof(ICommand));

		public IBotBuilder RegisterFactories()
			=> RegisterInheritedTypes(typeof(IFactory));

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
		
		public IBotBuilder ConfigureDb<TDb>(Action<DbContextOptionsBuilder<TDb>> builder) 
			where TDb : DbContext, IBotDatabase
		{
			var contextBuilder = new DbContextOptionsBuilder<TDb>();
			builder(contextBuilder);
			
			_builder.RegisterType<TDb>()
					.WithParameter("options", contextBuilder.Options)
					.AsSelf()
					.SingleInstance();
			return this;
		}

		public IBot Build()
		{
			var bot = _builder.Build().Resolve<Bot>();
			return bot;
		}

		private IBotBuilder RegisterInheritedTypes(Type type)
		{
			_builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
					.PublicOnly()
					.Where(t => t.InheritedFrom(type))
					.AsImplementedInterfaces()
					.AsSelf()
					.SingleInstance();
			return this;
		}
	}
}