using System;
using Botted.Core.Abstractions.Services;
using Botted.Plugins.Permissions.Data;

namespace Botted.Plugins.Permissions.Abstractions
{
	public interface IPermissionsService : IService
	{
		Permission CreatePermission(string permissionName);
		void ConfigureInitialPermissions(Action<PermissionsBuilder> configurator);
	}
}