using Autofac;
using Botted.Core.Abstractions.Services.Commands.Events;
using Botted.Core.Abstractions.Services.Database;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Providers.Events;
using Botted.Core.Abstractions.Services.Users;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Abstractions.Services.Users.Events;
using Botted.Core.Factories;
using Botted.Core.Services.Commands;
using Botted.Core.Services.Users;
using Botted.Plugins.Permissions;
using Botted.Plugins.Permissions.Abstractions;
using Botted.Plugins.Permissions.Data;
using Botted.Plugins.Permissions.Exceptions;
using Botted.Plugins.Permissions.Extensions;
using Botted.Tests.TestEnvironment;
using Botted.Tests.TestEnvironment.Commands;
using FakeItEasy;
using NUnit.Framework;

namespace Botted.Tests.PluginTests
{
	public class PermissionsTest : BaseTest
	{
		[Test]
		public void CreatingTest()
		{
			Container.BeginLifetimeScope();
			var service = Container.Resolve<PermissionsService>();
			
			var permission1 = service.CreatePermission("test1");
			Assert.AreEqual("test1", permission1.Name);
			
			var permission2 = service.CreatePermission("test2");
			Assert.AreEqual("test2", permission2.Name);

			Assert.Throws<PermissionException>(() => service.CreatePermission("test1"));
		}
		
		[Test]
		public void MatchingTest()
		{
			Container.BeginLifetimeScope();
			var service = Container.Resolve<PermissionsService>();
			var permission1 = service.CreatePermission("test1");
			var permission2 = service.CreatePermission("test2");
			
			Assert.True(permission1.IsMatching(Permission.All));
			Assert.True(permission2.IsMatching(Permission.All));
			Assert.False(permission1.IsMatching(permission2));
			Assert.True((permission1 | permission2).IsMatching(permission1));
			Assert.True((permission1 | permission2).IsMatching(permission2));
		}

		[Test]
		public void InitialTest()
		{
			Container.BeginLifetimeScope();
			var eventService = Container.Resolve<TestEventService>();
			var service = Container.Resolve<PermissionsService>();
			var permission1 = service.CreatePermission("test1");
			
			eventService.Rise<UserRegistered, BotUser>(new BotUser());
			var user = eventService.GetLastData<UserRegistered, BotUser>();
			Assert.False(user.HasPermission(permission1));
			
			service.ConfigureInitialPermissions(p => p.AddPermission(permission1));
			
			eventService.Rise<UserRegistered, BotUser>(new BotUser());
			user = eventService.GetLastData<UserRegistered, BotUser>();
			Assert.True(user.HasPermission(permission1));
			
			var permission2 = service.CreatePermission("test2");
			service.ConfigureInitialPermissions(p => p.RemovePermission(permission1)
													  .AddPermission(permission2));
			
			eventService.Rise<UserRegistered, BotUser>(new BotUser());
			user = eventService.GetLastData<UserRegistered, BotUser>();
			Assert.False(user.HasPermission(permission1));
			Assert.True(user.HasPermission(permission2));
		}

		[Test]
		public void GrantAndTakeTest()
		{
			Container.BeginLifetimeScope();
			var user = new BotUser();
			var service = Container.Resolve<PermissionsService>();
			var permission = service.CreatePermission("test");

			Assert.DoesNotThrow(() => user.GrantPermission(permission));
			Assert.True(user.HasPermission(permission));
			Assert.Throws<PermissionAlreadyGrantedException>(() => user.GrantPermission(permission));
			
			Assert.DoesNotThrow(() => user.TakePermission(permission));
			Assert.False(user.HasPermission(permission));
			Assert.Throws<NoSuchPermissionException>(() => user.TakePermission(permission));
		}

		[Test]
		public void ExecutePermissionsTest()
		{
			Container.BeginLifetimeScope();
			var user = new BotUser();
			var provider = Container.Resolve<TestProvider>();
			var service = Container.Resolve<PermissionsService>();
			var command = Container.Resolve<SimpleTestCommand>();
			var permission = service.CreatePermission("test");

			command.ConfigurePermissions(permission);
			provider.ReceiveMessage("!test", user);
			Assert.Null(provider.LastSentMessage);
			
			user.GrantPermission(permission);
			provider.ReceiveMessage("!test", user);
			Assert.NotNull(provider.LastSentMessage);
			Assert.AreEqual("Success!", provider.LastSentMessage.Text);
		}
	}
}