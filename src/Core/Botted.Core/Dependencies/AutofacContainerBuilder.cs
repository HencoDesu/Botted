using System;
using Autofac;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Abstractions.Extensions;
using Microsoft.Extensions.Configuration;
using IContainer = Botted.Core.Abstractions.Dependencies.IContainer;

namespace Botted.Core.Dependencies
{
	public class AutofacContainerBuilder : IContainerBuilder
	{
		private readonly ContainerBuilder _containerBuilder;

		public AutofacContainerBuilder(ContainerBuilder containerBuilder)
		{
			_containerBuilder = containerBuilder;
		}

		public void RegisterService<T>() where T : IService
		{
			RegisterService<T, T>();
		}

		public void RegisterService<TAbstraction, TImplementation>() 
			where TImplementation : TAbstraction
			where TAbstraction : IService
		{
			RegisterSingleton<TAbstraction, TImplementation>();
		}

		public void RegisterSingleton<T>(Func<IContainer, T> creator)
			where T : notnull
		{
			RegisterSingleton<T, T>(creator);
		}

		public void RegisterSingleton<T>(T instance)
			where T : class
		{
			RegisterSingleton<T, T>(instance);
		}

		public void RegisterSingleton<T>()
			where T : notnull
		{
			RegisterSingleton<T, T>();
		}

		public void RegisterSingleton<TAbstraction, TImplementation>(Func<IContainer, TImplementation> creator) 
			where TImplementation : TAbstraction
			where TAbstraction : notnull
		{
			_containerBuilder.Register(c => WithWrapped(c, creator))
							 .As<TImplementation>()
							 .As<TAbstraction>()
							 .SingleInstance();
		}

		public void RegisterSingleton<TAbstraction, TImplementation>(TImplementation instance) 
			where TImplementation : class, TAbstraction
			where TAbstraction : class
		{
			_containerBuilder.RegisterInstance(instance)
							 .As<TAbstraction>()
							 .As<TImplementation>()
							 .SingleInstance();
		}

		public void RegisterSingleton<TAbstraction, TImplementation>() 
			where TImplementation : TAbstraction
			where TAbstraction : notnull
		{
			_containerBuilder.RegisterType<TImplementation>()
							 .As<TAbstraction>()
							 .SingleInstance()
							 .AutoActivate();
		}

		public void RegisterConfigurationSection<TConfigSection>(string sectionName)
			where TConfigSection : notnull
		{
			_containerBuilder.Register(c => c.Resolve<IConfiguration>()
											 .GetSection(sectionName)
											 .Get<TConfigSection>())
							 .AsSelf()
							 .SingleInstance()
							 .AutoActivate();
		}

		public void RegisterBuildCallback(Action<IContainer> callback)
		{
			_containerBuilder.RegisterBuildCallback(scope => WithWrapped(scope, callback));
		}

		private static void WithWrapped(IComponentContext scope, Action<IContainer> action)
		{
			new AutofacContainer(scope).Apply(action);
		}

		private static T WithWrapped<T>(IComponentContext scope, Func<IContainer, T> func)
		{
			var wrapped = new AutofacContainer(scope);
			return func(wrapped);
		}
	}
}