using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Botted.Core.Events.Abstractions;
using Botted.Core.Users.Abstractions;
using Botted.Core.Users.Abstractions.Data;
using Botted.Core.Users.Abstractions.Events;

namespace Botted.Core.Users
{
	/// <inheritdoc />
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "It's a service that will be initialized via DI container")]
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

		public User GetById(long userId)
			=> _database.Users.SingleOrDefault(u => u.Id == userId) ?? throw new Exception(); //TODO: Custom exception here

		public IReadOnlyCollection<User> GetAll() 
			=> _database.Users.ToList();

		public IReadOnlyCollection<User> GetAll(Func<User, bool> predicate)
			=> _database.Users
						.Where(u => predicate(u))
						.ToList();

		public User Register()
		{
			var user = _database.RegisterUser();
			_eventService.Raise<UserRegistered, User>(user);
			return user;
		}
	}
}