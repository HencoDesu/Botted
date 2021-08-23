using System.Diagnostics.CodeAnalysis;
using Botted.Core.Users.Abstractions;
using Botted.Core.Users.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Users
{
	/// <inheritdoc cref="Botted.Core.Users.Abstractions.IUserDatabase" />
	public class UserDatabase
		: DbContext, IUserDatabase
	{
		public UserDatabase(DbContextOptions<UserDatabase> options) 
			: base(options)
		{ }

		/// <inheritdoc />
		[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
		[SuppressMessage("ReSharper", "ReplaceAutoPropertyWithComputedProperty")]
		public DbSet<User> Users { get; } = null!;

		/// <inheritdoc />
		public new void SaveChanges()
			=> base.SaveChanges();
	}
}