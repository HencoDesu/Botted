using System;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Plugins.Permissions.Exceptions;
using NLog;

namespace Botted.Plugins.Permissions
{
	public static class BotUserExtensions
	{
		private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
		
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
				_logger.Error(_);
			}
			
			throw new PermissionAlreadyGrantedException($"User already have permission {permission}");
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
				_logger.Error(_);
			}
			
			throw new NoSuchPermissionException($"User doesn't have permission {permission}");
		}
	}
}