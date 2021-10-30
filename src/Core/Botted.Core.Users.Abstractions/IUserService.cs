using System;
using System.Collections.Generic;
using Botted.Core.Abstractions;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Users.Abstractions
{
	/// <summary>
	/// Represents a service to receive information about users
	/// </summary>
	public interface IUserService : IBottedService
	{
		/// <summary>
		/// Gets user with specific ID
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <returns>User with ID</returns>
		BottedUser GetById(long userId);
		
		/// <summary>
		/// Gets all users
		/// </summary>
		/// <returns>All users</returns>
		IReadOnlyCollection<BottedUser> GetAll();
		
		/// <summary>
		/// Gets all users that matches predicate
		/// </summary>
		/// <param name="predicate">Predicate to filter users</param>
		/// <returns>Users that matches predicate</returns>
		IReadOnlyCollection<BottedUser> GetAll(Func<BottedUser, bool> predicate);
		
		/// <summary>
		/// Register a new user
		/// </summary>
		/// <returns>New user</returns>
		BottedUser Register();
	}
}