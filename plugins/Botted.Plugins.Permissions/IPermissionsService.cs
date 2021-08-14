using System;
using Botted.Core.Abstractions.Services;
using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Plugins.Permissions
{
	public interface IPermissionsService : IService
	{
		Permission CreatePermission(string permissionName);
		bool HasPermission(BotUser user, Permission permission);
		void GrantPermission(BotUser user, Permission permission);
		void TakePermission(BotUser user, Permission permission);
		void ConfigureInitialPermissions(Action<PermissionsBuilder> configurator);
	}
}