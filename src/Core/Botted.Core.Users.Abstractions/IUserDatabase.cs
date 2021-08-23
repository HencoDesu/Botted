using Botted.Core.Database.Abstractions;
using Botted.Core.Users.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Users.Abstractions
{
	/// <summary>
	/// Represents a database for <see cref="User"/>
	/// </summary>
	public interface IUserDatabase : IDatabase
	{
		/// <summary>
		/// Users of the bot
		/// </summary>
		DbSet<User> Users { get; }
	}
}