using System;
using Botted.Core.Abstractions.Services.Database;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Abstractions.Services.Users.Events;
using Botted.Core.Services.Database;
using Botted.Core.Services.Users;
using Botted.Plugins.Permissions;
using Botted.Tests.TestEnvironment;
using FakeItEasy;
using NUnit.Framework;

namespace Botted.Tests.PluginTests
{
	public class PermissionsTest
	{
		[Test]
		public void CreatingTest()
		{
			var service = new PermissionsService(A.Fake<IEventService>());
			
			var permission1 = service.CreatePermission("test1");
			Assert.AreEqual("test1", permission1.Name);
			
			var permission2 = service.CreatePermission("test2");
			Assert.AreEqual("test2", permission2.Name);

			Assert.Throws<Exception>(() => service.CreatePermission("test1"));
		}
		
		[Test]
		public void MatchingTest()
		{
			var service = new PermissionsService(A.Fake<IEventService>());
			var permission1 = service.CreatePermission("test1");
			var permission2 = service.CreatePermission("test2");
			
			Assert.True(permission1.IsMatching(Permission.All));
			Assert.True(permission2.IsMatching(Permission.All));
			Assert.False(permission1.IsMatching(permission2));
			Assert.True(permission1.IsMatching(permission1 | permission2));
			Assert.True(permission2.IsMatching(permission1 | permission2));
		}

		[Test]
		public void InitialTest()
		{
			var eventService = new TestEventService(new UserRegistered());
			var userService = new UserService(A.Fake<IBotDatabase>(), eventService);
			var service = new PermissionsService(eventService);
			var permission1 = service.CreatePermission("test1");
			
			userService.RegisterUser(_ => { });
			var user = eventService.GetLastData<UserRegistered, BotUser>();
			Assert.False(user.HasPermission(permission1));
			
			service.ConfigureInitialPermissions(p => p.AddPermission(permission1));
			
			userService.RegisterUser(_ => { });
			user = eventService.GetLastData<UserRegistered, BotUser>();
			Assert.True(user.HasPermission(permission1));
		}

		[Test]
		public void GrantAndTakeTest()
		{
			var user = new BotUser();
			var service = new PermissionsService(A.Fake<IEventService>());
			var permission = service.CreatePermission("test");
			
			user.GrantPermission(permission);
			Assert.True(user.HasPermission(permission));

			user.TakePermission(permission);
			Assert.False(user.HasPermission(permission));

			Assert.Throws<Exception>(() => user.TakePermission(permission));
		}
	}
}