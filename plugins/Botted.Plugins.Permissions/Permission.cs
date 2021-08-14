using System.Collections.Generic;

namespace Botted.Plugins.Permissions
{
	public class Permission
	{
		public static Permission All { get; } = new("*");

		private readonly List<Permission> _additionalPermissions = new();

		internal Permission(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public static Permission operator |(Permission left, Permission right)
		{
			if (left._additionalPermissions.Count > 0)
			{
				left.AddPermission(right);
				return left;
			}

			var combined = new Permission($"{left}|{right}");
			combined.AddPermission(left);
			combined.AddPermission(right);
			return combined;
		}

		public bool IsMatching(Permission permission)
			=> permission.Name == All.Name 
			|| permission.Name == Name
			|| _additionalPermissions.Contains(permission)
			|| permission._additionalPermissions.Contains(this);

		private void AddPermission(Permission additionalIdentifier)
			=> _additionalPermissions.Add(additionalIdentifier);

		public override string ToString() => Name;
	}
}