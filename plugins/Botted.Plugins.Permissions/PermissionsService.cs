using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Abstractions.Services.Users.Events;
using Botted.Plugins.Permissions.Exceptions;

namespace Botted.Plugins.Permissions
{
	public class PermissionsService : IPermissionsService
	{
		public static PermissionsBuilder InitialPermissionsBuilder { get; } = new();
		
		private readonly HashSet<Permission> _permissions = new();

		public PermissionsService(IEventService eventService)
		{
			eventService.Subscribe<UserRegistered, BotUser>(OnUserRegistered);
		}

		public Permission CreatePermission(string permissionName)
		{
			if (_permissions.Any(p => p.Name == permissionName))
			{
				throw new PermissionException("Permission with the same name was already registered");
			}

			var permission = new Permission(permissionName);
			_permissions.Add(permission);
			return permission;
		}

		public void ConfigureInitialPermissions(Action<PermissionsBuilder> configurator)
			=> configurator(InitialPermissionsBuilder);

		private void OnUserRegistered(BotUser user)
		{
			var permissions = InitialPermissionsBuilder.Build();
			user.SaveAdditionalData(permissions);
		}
	}
}