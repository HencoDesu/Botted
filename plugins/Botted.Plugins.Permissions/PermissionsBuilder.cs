using System;
using System.Collections.Generic;
using Botted.Core.Abstractions;

namespace Botted.Plugins.Permissions
{
	public class PermissionsBuilder : IBuilder<PermissionsData>
	{
		private readonly HashSet<Permission> _permissions = new();

		public PermissionsBuilder AddPermission(Permission permission, bool ignoreException = false)
		{
			if (!_permissions.Add(permission) && !ignoreException)
			{
				//TODO: Custom exception here
				throw new Exception($"Permission {permission} already added");
			}

			return this;
		}
		
		public PermissionsBuilder RemovePermission(Permission permission, bool ignoreException = false)
		{
			if (!_permissions.Remove(permission) && !ignoreException)
			{
				//TODO: Custom exception here
				throw new Exception($"Permission {permission} wasn't added before");
			}

			return this;
		}

		public PermissionsData Build() 
			=> new(_permissions);
	}
}