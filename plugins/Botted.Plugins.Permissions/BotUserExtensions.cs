using System;
using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Plugins.Permissions
{
	public static class BotUserExtensions
	{
		public static bool HasPermission(this BotUser user, Permission permission)
		{
			var permissions = user.GetAdditionalData<PermissionsData>();
			return permissions?.Has(permission) ?? false;
		}

		public static void GrantPermission(this BotUser user, Permission permission)
		{
			try
			{
				var permissions = user.GetAdditionalData<PermissionsData>();
				if (permissions is null)
				{
					permissions = PermissionsService.InitialPermissionsBuilder.Build();
					user.SaveAdditionalData(permissions);
				}
				if (permissions.Grant(permission))
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

		public static void TakePermission(this BotUser user, Permission permission)
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
			throw new Exception($"User doesn't have permission {permission}");
		}
	}
}