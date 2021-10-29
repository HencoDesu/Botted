using System;
using Botted.Core.Abstractions;
using Botted.Core.Database.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Users.Abstractions.Extensions
{
	public static class BottedBuilderExtensions
	{
		public static IBottedBuilder UseUserService<TUserService>(this IBottedBuilder bottedBuilder) 
			where TUserService : IUserService
		{
			return bottedBuilder.ConfigureContainer(c => c.RegisterService<IUserService, TUserService>());
		}

		public static IBottedBuilder UseUserDatabase<TUserDatabase>(this IBottedBuilder bottedBuilder,
																	Action<DbContextOptionsBuilder<TUserDatabase>>? options = null) 
			where TUserDatabase : DbContext, IUserDatabase
		{
			return bottedBuilder.UseDatabase<IUserDatabase, TUserDatabase>(options);
		}
	}
}