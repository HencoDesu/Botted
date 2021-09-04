using System;
using Botted.Core.Abstractions.Builders;
using Botted.Core.Users.Abstractions;
using Botted.Core.Users.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Users.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Use default implementation of <see cref="IUserService"/>
		/// </summary>
		/// <param name="builder">Current <see cref="IBotBuilder"/></param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseDefaultUserService(this IBotBuilder builder) 
			=> builder.UseUserService<UserService>();

		/// <summary>
		/// Use default implementation of <see cref="IUserDatabase"/>
		/// </summary>
		/// <param name="builder">Current <see cref="IBotBuilder"/></param>
		/// <param name="configure">Context options builder</param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseDefaultUserDatabase(this IBotBuilder builder, 
														 Action<DbContextOptionsBuilder<UserDatabase>> configure)
			=> builder.UseUserDatabase(configure);
	}
}