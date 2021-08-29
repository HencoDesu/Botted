using System.Collections.Generic;
using System.Linq;

namespace Botted.Plugins.Permissions.Data
{
	public class PermissionsData
	{
		private readonly HashSet<Permission> _permissions;

		public PermissionsData() 
			: this(new HashSet<Permission>()) {}
		
		public PermissionsData(HashSet<Permission> permissions)
		{
			_permissions = permissions;
		}

		public static implicit operator PermissionsData(Permission permission)
		{
			var data = new PermissionsData();
			data.Grant(permission);
			return data;
		}
		
		public static PermissionsData operator |(PermissionsData left, Permission right)
		{
			left.Grant(right);
			return left;
		}

		public bool IsMatching(PermissionsData other) 
			=> other._permissions.All(Has);

		public bool Has(Permission permission) 
			=> _permissions.Contains(permission);

		public bool Grant(Permission permission)
			=> _permissions.Add(permission);

		public bool Take(Permission permission)
			=> _permissions.Remove(permission);
	}
}