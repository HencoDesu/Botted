﻿using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Abstractions.Services.Database;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Users;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Abstractions.Services.Users.Events;
using Botted.Core.Abstractions.Services.Users.Exceptions;

namespace Botted.Core.Services.Users
{
	public class UserService : IUserService
	{
		private readonly IBotDatabase _database;
		private readonly IEventService _eventService;

		public UserService(IBotDatabase database, IEventService eventService)
		{
			_database = database;
			_eventService = eventService;
		}

		public BotUser GetUserById(ulong userId)
		{
			var user = _database.Users.SingleOrDefault(u => u.Id == userId);
			return user ?? throw new UserNotFoundException($"User with id {userId} not found");
		}

		public IReadOnlyCollection<BotUser> GetUsers()
			=> _database.Users.ToList();

		public void RegisterUser(Action<BotUser> configurator)
		{
			var user = new BotUser();
			_database.Users.Add(user);
			configurator(user);
			_eventService.Rise<UserRegistered, BotUser>(user);
			_database.SaveChanges();
		}
	}
}