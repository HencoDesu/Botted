using System.Collections.Generic;
using Botted.Core.Abstractions.Data;

namespace Botted.Plugins.Permissions
{
	public class PermissionsData : IAdditionalData
	{
		private readonly HashSet<Permission> _permissions;

		public PermissionsData() : this(new HashSet<Permission>()) {}
		
		public PermissionsData(HashSet<Permission> permissions)
		{
			_permissions = permissions;
		}
		
		public bool Has(Permission permission) 
			=> _permissions.Contains(permission);

		public bool Grant(Permission permission)
			=> _permissions.Add(permission);

		public bool Take(Permission permission)
			=> _permissions.Remove(permission);
	}
}