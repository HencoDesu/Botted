using System;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Database.Abstractions.Extensions
{
	public static class BottedBuilderExtensions
	{
		public static IBottedBuilder UseDatabase<TAbstraction, TDatabase>(this IBottedBuilder bottedBuilder,
																		  Action<DbContextOptionsBuilder<TDatabase>>? options = null)
			where TDatabase : DbContext, TAbstraction
			where TAbstraction : IDatabase
		{
			var optionsBuilder = new DbContextOptionsBuilder<TDatabase>();

			if (options is not null)
			{
				optionsBuilder.Apply(options);
			}

			return bottedBuilder.ConfigureServices(c =>
			{
				c.RegisterSingleton<TAbstraction, TDatabase>();
				c.RegisterSingleton(optionsBuilder.Options);
			});
		}
	}
}