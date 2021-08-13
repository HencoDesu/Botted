using System;
using System.Collections.Generic;
using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Core.Abstractions.Services.Users
{
	public interface IUserService : IService
	{
		BotUser GetUserById(ulong userId);
		IReadOnlyCollection<BotUser> GetUsers();
		void RegisterUser(Action<BotUser> configurator);
	}
}