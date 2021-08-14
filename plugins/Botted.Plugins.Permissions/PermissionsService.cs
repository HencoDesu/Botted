using System;
using System.Collections.Generic;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Abstractions.Services.Users.Events;

namespace Botted.Plugins.Permissions
{
	public class PermissionsService : IPermissionsService
	{
		private readonly HashSet<Permission> _permissions = new();
		private readonly PermissionsBuilder _initialPermissionsBuilder = new();

		public PermissionsService(IEventService eventService)
		{
			eventService.Subscribe<UserRegistered, BotUser>(OnUserRegistered);
		}

		public Permission CreatePermission(string permissionName)
		{
			var permission = new Permission(permissionName);
			if (!_permissions.Add(permission))
			{
				//TODO: Custom exception here
				throw new Exception("Permission with the same name was already registered");
			}

			return permission;
		}

		public bool HasPermission(BotUser user, Permission permission)
		{
			var permissions = user.GetAdditionalData<PermissionsData>();
			return permissions?.Has(permission) ?? false;
		}

		public void GrantPermission(BotUser user, Permission permission)
		{
			try
			{
				var permissions = user.GetAdditionalData<PermissionsData>();
				if (permissions!.Grant(permission))
				{
					return;
				}
			} catch (Exception _)
			{
				//TODO: Add logging here
			}
			
			//TODO: custom exception here
			throw new Exception($"User already have permission {permission}");
		}

		public void TakePermission(BotUser user, Permission permission)
		{
			try
			{
				var permissions = user.GetAdditionalData<PermissionsData>();
				if (permissions!.Take(permission))
				{
					return;
				}
			} catch (Exception _)
			{
				//TODO: Add logging here
			}
			
			//TODO: custom exception here
			throw new Exception($"User already have permission {permission}");
		}

		public void ConfigureInitialPermissions(Action<PermissionsBuilder> configurator)
			=> configurator(_initialPermissionsBuilder);

		private void OnUserRegistered(BotUser user)
		{
			var permissions = _initialPermissionsBuilder.Build();
			user.SaveAdditionalData(permissions);
		}
	}
}