using System;
using Autofac;
using Botted.Core.Abstractions.Builders;
using Botted.Core.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Database.Abstractions.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Register database to DI container
		/// </summary>
		/// <param name="builder">Current <see cref="IBotBuilder"/></param>
		/// <param name="configure">Context options builder</param>
		/// <typeparam name="TAbstraction">Database abstraction</typeparam>
		/// <typeparam name="TDatabase">Database implementation</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseDatabase<TAbstraction, TDatabase>(
			this IBotBuilder builder,
			Action<DbContextOptionsBuilder<TDatabase>> configure)
			where TAbstraction : IDatabase
			where TDatabase : DbContext, TAbstraction
		{
			return builder.RegisterSingleton<TAbstraction, TDatabase>()
						  .ConfigureServices(
							  b => b.RegisterInstance(BuildOptions(configure))
									.AsSelf()
									.SingleInstance());
		}

		private static DbContextOptions<TContext> BuildOptions<TContext>(
			Action<DbContextOptionsBuilder<TContext>> configure)
			where TContext : DbContext
		{
			return new DbContextOptionsBuilder<TContext>().Apply(configure).Options;
		}
	}
}