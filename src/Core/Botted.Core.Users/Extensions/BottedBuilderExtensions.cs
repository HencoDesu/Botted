using System;
using Botted.Core.Abstractions;
using Botted.Core.Users.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Users.Extensions
{
	public static class BottedBuilderExtensions
	{
		public static IBottedBuilder UseDefaultUserService(this IBottedBuilder bottedBuilder)
		{
			return bottedBuilder.UseUserService<UserService>();
		}

		public static IBottedBuilder UseDefaultUserDatabase(this IBottedBuilder bottedBuilder, 
															Action<DbContextOptionsBuilder<UserDatabase>>? options = null)
		{
			return bottedBuilder.UseUserDatabase(options);
		}
	}
}