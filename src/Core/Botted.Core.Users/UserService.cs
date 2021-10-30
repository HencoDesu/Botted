﻿using System;
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
		private readonly IEventBottedService _eventBottedService;

		public UserService(IUserDatabase database, 
						   IEventBottedService eventBottedService)
		{
			_database = database;
			_eventBottedService = eventBottedService;
		}

		public BottedUser GetById(long userId)
			=> _database.Users.SingleOrDefault(u => u.Id == userId) ?? throw new Exception(); //TODO: Custom exception here

		public IReadOnlyCollection<BottedUser> GetAll() 
			=> _database.Users.ToList();

		public IReadOnlyCollection<BottedUser> GetAll(Func<BottedUser, bool> predicate)
			=> _database.Users
						.Where(u => predicate(u))
						.ToList();

		public BottedUser Register()
		{
			var user = _database.RegisterUser();
			_eventBottedService.GetEvent<UserRegistered>().Raise(user);
			return user;
		}
	}
}