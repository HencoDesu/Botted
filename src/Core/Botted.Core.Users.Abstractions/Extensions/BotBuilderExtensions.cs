using System;
using Botted.Core.Abstractions;
using Botted.Core.Database.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Users.Abstractions.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Register selected <see cref="IUserService"/> as default
		/// </summary>
		/// <param name="builder">Current <see cref="IBotBuilder"/></param>
		/// <typeparam name="TUserService">Provider</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseUserService<TUserService>(this IBotBuilder builder) 
			where TUserService : IUserService 
			=> builder.RegisterService<IUserService, TUserService>();

		/// <summary>
		/// Register <see cref="IUserDatabase"/> implementation
		/// </summary>
		/// <param name="builder">Current <see cref="IBotBuilder"/></param>
		/// <param name="configure">Context options builder</param>
		/// <typeparam name="TUserDatabase">Database implementation</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseUserDatabase<TUserDatabase>(this IBotBuilder builder,
																 Action<DbContextOptionsBuilder<TUserDatabase>> configure)
			where TUserDatabase : DbContext, IUserDatabase
			=> builder.UseDatabase<IUserDatabase, TUserDatabase>(configure);
	}
}