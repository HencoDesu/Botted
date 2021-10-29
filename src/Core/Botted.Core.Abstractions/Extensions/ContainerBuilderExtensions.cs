﻿using Autofac;
using Microsoft.Extensions.Configuration;

namespace Botted.Core.Abstractions.Extensions
{
	public static class ContainerBuilderExtensions
	{
		/// <summary>
		/// Shortcut to register any type as auto activated singleton
		/// </summary>
		/// <param name="builder">Builder to register type</param>
		/// <typeparam name="TSingleton">Type to register</typeparam>
		/// <returns>Current <see cref="ContainerBuilder"/></returns>
		public static ContainerBuilder RegisterSingleton<TSingleton>(this ContainerBuilder builder)
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
		/// <returns>Current <see cref="ContainerBuilder"/></returns>
		public static ContainerBuilder RegisterSingleton<TAbstraction, TImplementation>(this ContainerBuilder builder)
			where TAbstraction : notnull
			where TImplementation : TAbstraction
		{
			builder.RegisterType<TImplementation>()
				   .As<TAbstraction>()
				   .SingleInstance()
				   .AutoActivate();

			return builder;
		}

		/// <summary>
		/// Register configuration section
		/// </summary>
		/// <param name="builder">Builder to register config</param>
		/// <param name="sectionName">Config section name</param>
		/// <typeparam name="TConfiguration">Config type</typeparam>
		/// <returns>Current <see cref="ContainerBuilder"/></returns>
		public static ContainerBuilder RegisterConfiguration<TConfiguration>(
			this ContainerBuilder builder, string sectionName)
			where TConfiguration : notnull
		{
			

			return builder;
		}
	}
}