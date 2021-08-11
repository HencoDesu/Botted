using System;
using System.IO;
using System.Reflection;
using Autofac;
using Booted.Core.Abstractions;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Dependencies.Attributes;
using Booted.Core.Events;
using Booted.Core.Events.Abstractions;
using Booted.Core.Extensions;

namespace Booted.Core.Bot
{
	public class BotBuilder : IBuilder<Bot>
	{
		private readonly ContainerBuilder _builder = new();

		public BotBuilder()
		{
			_builder.RegisterType<Bot>()
					.AsSelf();
		}

		public BotBuilder UseDefaultEventService()
			=> UseEventService<EventService>();

		public BotBuilder UseEventService<T>()
			where T : class, IEventService
		{
			_builder.RegisterType<T>()
					.As<IEventService>()
					.SingleInstance();
			return this;
		}

		public BotBuilder LoadPlugins()
		{
			var pluginsPath = Path.Combine(Environment.CurrentDirectory, "Plugins");
			var allPlugins = new DirectoryInfo(pluginsPath).GetFiles();
			foreach (var plugin in allPlugins)
			{
				Assembly.LoadFile(plugin.FullName);
			}

			return this;
		}

		public BotBuilder RegisterEvents()
		{
			_builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
					.PublicOnly()
					.Where(t => t.InheritedFrom(typeof(IEvent)))
					.AsImplementedInterfaces()
					.SingleInstance();
			return this;
		}

		public BotBuilder RegisterServices()
		{
			_builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
					.PublicOnly()
					.Where(t => t.GetCustomAttribute<ServiceAttribute>() != null)
					.AsImplementedInterfaces()
					.SingleInstance();
			return this;
		}

		public BotBuilder RegisterCommands()
		{
			_builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
					.PublicOnly()
					.Where(t => t.InheritedFrom(typeof(ICommand)))
					.AsImplementedInterfaces()
					.SingleInstance();
			return this;
		}

		public Bot Build()
		{
			var bot = _builder.Build().Resolve<Bot>();
			return bot;
		}
	}
}