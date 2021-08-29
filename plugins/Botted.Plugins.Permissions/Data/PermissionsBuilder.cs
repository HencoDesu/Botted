using System.Collections.Generic;
using Botted.Core.Abstractions;
using Botted.Plugins.Permissions.Exceptions;

namespace Botted.Plugins.Permissions.Data
{
	public class PermissionsBuilder
	{
		private readonly HashSet<Permission> _permissions = new();

		public PermissionsBuilder AddPermission(Permission permission, bool ignoreException = false)
		{
			if (!_permissions.Add(permission) && !ignoreException)
			{
				throw new PermissionException($"Permission {permission} already added");
			}

			return this;
		}
		
		public PermissionsBuilder RemovePermission(Permission permission, bool ignoreException = false)
		{
			if (!_permissions.Remove(permission) && !ignoreException)
			{
				throw new PermissionException($"Permission {permission} wasn't added before");
			}

			return this;
		}

		public PermissionsData Build() 
			=> new(_permissions);
	}
}