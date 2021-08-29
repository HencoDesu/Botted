namespace Botted.Plugins.Permissions.Data
{
	public class Permission
	{
		public static Permission All { get; } = new("*");

		internal Permission(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public static PermissionsData operator |(Permission left, Permission right)
		{
			PermissionsData data = left;
			data.Grant(right);
			return data;
		}

		public bool IsMatching(Permission permission)
			=> permission.Name == All.Name
			|| permission.Name == Name;

		public override string ToString() => Name;

		public override int GetHashCode() => Name.GetHashCode();
	}
}