using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Events.Abstractions;
using Botted.Core.Users.Abstractions;
using Botted.Core.Users.Abstractions.Data;
using Botted.Core.Users.Abstractions.Events;

namespace Botted.Core.Users
{
	/// <inheritdoc />
	public class UserService : IUserService
	{
		private readonly IUserDatabase _database;
		private readonly IEventService _eventService;

		public UserService(IUserDatabase database, 
						   IEventService eventService)
		{
			_database = database;
			_eventService = eventService;
		}

		public User GetById(ulong userId)
			=> _database.Users.Find(userId) ?? throw new Exception(); //TODO: Custom exception here

		public IReadOnlyCollection<User> GetAll() 
			=> _database.Users.ToList();

		public IReadOnlyCollection<User> GetAll(Func<User, bool> predicate)
			=> _database.Users
						.Where(predicate)
						.ToList();

		public User Register()
		{
			var user = new User();
			_database.Users.Add(user);
			_database.SaveChanges();
			_eventService.Raise<UserRegistered, User>(user);
			return user;
		}
	}
}