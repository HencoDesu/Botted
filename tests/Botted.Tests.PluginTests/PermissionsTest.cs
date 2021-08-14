using Botted.Core.Abstractions.Services.Commands.Events;
using Botted.Core.Abstractions.Services.Database;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Providers.Events;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Abstractions.Services.Users.Events;
using Botted.Core.Factories;
using Botted.Core.Services.Commands;
using Botted.Core.Services.Users;
using Botted.Plugins.Permissions;
using Botted.Plugins.Permissions.Data;
using Botted.Plugins.Permissions.Exceptions;
using Botted.Plugins.Permissions.Extensions;
using Botted.Tests.TestEnvironment;
using Botted.Tests.TestEnvironment.Commands;
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

			Assert.Throws<PermissionException>(() => service.CreatePermission("test1"));
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
			Assert.True((permission1 | permission2).IsMatching(permission1));
			Assert.True((permission1 | permission2).IsMatching(permission2));
		}

		[Test]
		public void InitialTest()
		{
			var eventService = new TestEventService(new UserRegistered(), new CommandExecuting());
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
			
			var permission2 = service.CreatePermission("test2");
			service.ConfigureInitialPermissions(p => p.RemovePermission(permission1)
													  .AddPermission(permission2));
			
			userService.RegisterUser(_ => { });
			user = eventService.GetLastData<UserRegistered, BotUser>();
			Assert.False(user.HasPermission(permission1));
			Assert.True(user.HasPermission(permission2));
		}

		[Test]
		public void GrantAndTakeTest()
		{
			var user = new BotUser();
			var service = new PermissionsService(A.Fake<IEventService>());
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
			var user = new BotUser();
			var eventService = new TestEventService(new UserRegistered(), new CommandExecuting(), new MessageReceived(), new NeedSendMessage());
			var service = new PermissionsService(eventService);
			var permission = service.CreatePermission("test");
			var command = new SimpleTestCommand();
			var commandsService = new CommandService(eventService, new [] {command}, new CommandResultFactory());
			var provider = new TestProvider(eventService);

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