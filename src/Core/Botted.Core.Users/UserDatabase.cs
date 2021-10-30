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
		IQueryable<BottedUser> IUserDatabase.Users => UsersTable;

		private DbSet<BottedUser> UsersTable { get; set; } = null!;

		/// <inheritdoc />
		public BottedUser RegisterUser()
		{
			var user = new BottedUser();
			UsersTable.Add(user);
			SaveChanges();
			return user;
		}

		/// <inheritdoc />
		public new void SaveChanges()
			=> base.SaveChanges();
	}
}