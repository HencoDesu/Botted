using System;
using Botted.Core.Abstractions.Services;

namespace Botted.Plugins.Permissions
{
	public interface IPermissionsService : IService
	{
		Permission CreatePermission(string permissionName);
		void ConfigureInitialPermissions(Action<PermissionsBuilder> configurator);
	}
}