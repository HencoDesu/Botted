using System;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Plugins.Permissions.Data;

namespace Botted.Plugins.Permissions.Extensions
{
	public static class CommandExtensions
	{
		public static ICommand ConfigurePermissions(this ICommand command,
													Permission permission)
			=> ConfigurePermissions(command, b => b.AddPermission(permission));
		
		public static ICommand ConfigurePermissions(this ICommand command, 
													Action<PermissionsBuilder> builder)
		{
			var permissionsBuilder = new PermissionsBuilder();
			builder(permissionsBuilder);
			command.SaveAdditionalData(permissionsBuilder.Build());
			return command;
		}
	}
}