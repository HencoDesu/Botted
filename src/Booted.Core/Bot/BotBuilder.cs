using System;
using System.IO;
using System.Reflection;
using Autofac;
using Booted.Core.Extensions;
using Botted.Core.Abstractions.Bot;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Events;

namespace Booted.Core.Bot
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
					.SingleInstance();
			return this;
		}
	}
}