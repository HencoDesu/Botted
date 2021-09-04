using Autofac;
using Botted.Core.Abstractions.Builders;

namespace Botted.Core.Abstractions.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Shortcut to register any service
		/// </summary>
		/// <param name="builder">Builder to register service</param>
		/// <typeparam name="TAbstraction">Service abstraction</typeparam>
		/// <typeparam name="TService">Service implementation</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder RegisterService<TAbstraction, TService>(this IBotBuilder builder)
			where TAbstraction : IService
			where TService : TAbstraction
		{
			return builder.ConfigureServices(b => b.RegisterType<TService>()
												   .As<TAbstraction>()
												   .As<IService>()
												   .SingleInstance()
												   .AutoActivate());
		}

		/// <summary>
		/// Shortcut to register any type as auto activated singleton
		/// </summary>
		/// <param name="builder">Builder to register type</param>
		/// <typeparam name="TSingleton">Type to register</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder RegisterSingleton<TSingleton>(this IBotBuilder builder)
			where TSingleton : notnull
		{
			return builder.RegisterSingleton<TSingleton, TSingleton>();
		}
		
		/// <summary>
		/// Shortcut to register any type as auto activated singleton
		/// </summary>
		/// <param name="builder">Builder to register type</param>
		/// <typeparam name="TAbstraction">Type abstraction</typeparam>
		/// <typeparam name="TImplementation">Type implementation</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder RegisterSingleton<TAbstraction, TImplementation>(this IBotBuilder builder)
			where TAbstraction : notnull
			where TImplementation : TAbstraction
		{
			return builder.ConfigureServices(b => b.RegisterType<TImplementation>()
												   .As<TAbstraction>()
												   .SingleInstance()
												   .AutoActivate());
		}
	}
}