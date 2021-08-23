using System;
using Autofac;
using Botted.Core.Abstractions;

namespace Botted.Core
{
	/// <inheritdoc />
	public class BotBuilder : IBotBuilder
	{
		private readonly ContainerBuilder _builder;

		public BotBuilder(ContainerBuilder builder)
		{
			_builder = builder;
		}

		/// <inheritdoc />
		public IBotBuilder RegisterService<TInterface, TImplementation>() 
			where TInterface : notnull
			where TImplementation : TInterface
		{
			_builder.RegisterType<TImplementation>()
					.As<TInterface>()
					.SingleInstance();
			
			return this;
		}
		
		/// <inheritdoc />
		public IBotBuilder RegisterService<TInterface, TImplementation>(TImplementation instance) 
			where TInterface : notnull
			where TImplementation : class, TInterface
		{
			_builder.RegisterInstance(instance)
					.As<TInterface>()
					.SingleInstance();
			
			return this;
		}
		
		/// <inheritdoc />
		public IBotBuilder RegisterService<TService>(TService instance) 
			where TService : class
		{
			_builder.RegisterInstance(instance)
					.AsSelf()
					.SingleInstance();
			
			return this;
		}

		/// <inheritdoc />
		public IBot BuildBot() => new Bot();
	}
}