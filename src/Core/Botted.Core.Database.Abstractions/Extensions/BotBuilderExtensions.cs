using System;
using Botted.Core.Abstractions;
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
		public static IBotBuilder UseDatabase<TAbstraction, TDatabase>(this IBotBuilder builder, 
																	   Action<DbContextOptionsBuilder<TDatabase>> configure)
			where TAbstraction : IDatabase
			where TDatabase : DbContext, TAbstraction
		{
			var contextBuilder = new DbContextOptionsBuilder<TDatabase>();
			configure(contextBuilder);
			
			return builder.RegisterService<TAbstraction, TDatabase>()
						  .RegisterService(contextBuilder.Options);
		}
	}
}