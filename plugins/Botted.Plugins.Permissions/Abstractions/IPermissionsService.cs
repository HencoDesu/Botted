using System;
using Botted.Core.Users.Abstractions.Data;
using Botted.Plugins.Permissions.Data;

namespace Botted.Plugins.Permissions.Abstractions
{
	public interface IPermissionsService
	{
		Permission CreatePermission(string permissionName);
		void ConfigureInitialPermissions(Action<PermissionsBuilder> configurator);
		bool HasPermission(User user, Permission permission);
		bool GrantPermission(User user, Permission permission);
		bool TakePermission(User user, Permission permission);
	}
}