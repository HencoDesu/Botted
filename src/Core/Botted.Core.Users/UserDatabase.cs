using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
		IQueryable<User> IUserDatabase.Users => UsersTable;

		private DbSet<User> UsersTable { get; set; }

		/// <inheritdoc />
		public User RegisterUser()
		{
			var user = new User();
			UsersTable.Add(user);
			SaveChanges();
			return user;
		}

		/// <inheritdoc />
		public new void SaveChanges()
			=> base.SaveChanges();
	}
}