using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Abstractions.Services.Commands.Data;
using Botted.Core.Abstractions.Services.Commands.Events;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Abstractions.Services.Users.Events;
using Botted.Core.Commands.Abstractions.Context;
using Botted.Core.Commands.Abstractions.Events;
using Botted.Core.Events.Abstractions;
using Botted.Core.Users.Abstractions.Data;
using Botted.Core.Users.Abstractions.Events;
using Botted.Plugins.Permissions.Abstractions;
using Botted.Plugins.Permissions.Data;
using Botted.Plugins.Permissions.Exceptions;

namespace Botted.Plugins.Permissions
{
	public class PermissionsService : IPermissionsService
	{
		public static PermissionsBuilder InitialPermissionsBuilder { get; } = new();
		
		private readonly HashSet<Permission> _permissions = new();

		public PermissionsService(IEventService eventService)
		{
			eventService.Subscribe<UserRegistered, User>(OnUserRegistered);
			eventService.Subscribe<CommandExecuting, ICommandExecutingContext>(OnCommandExecuting);
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

		/// <inheritdoc />
		public bool HasPermission(User user, Permission permission)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public bool GrantPermission(User user, Permission permission)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public bool TakePermission(User user, Permission permission)
		{
			throw new NotImplementedException();
		}

		private void OnUserRegistered(User user)
		{
			var permissions = InitialPermissionsBuilder.Build();
			user.SaveAdditionalData(permissions);
		}

		private void OnCommandExecuting(ICommandExecutingContext context)
		{
			if (context.CanExecute == false) return;
			
			var requiredPermissions = context.Command.GetAdditionalData<PermissionsData>();
			if (requiredPermissions is null) return;
			
			var userPermissions = context.User.GetAdditionalData<PermissionsData>();
			if (userPermissions is null || !userPermissions.IsMatching(requiredPermissions))
			{
				context.CanExecute = false;
			}
		}
	}
}