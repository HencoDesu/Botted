using Autofac;
using Botted.Core.Abstractions.Services.Users.Events;
using Botted.Core.Services.Users;
using Botted.Tests.TestEnvironment;
using Botted.Tests.TestEnvironment.Data;
using NUnit.Framework;

namespace Botted.Tests.CoreTests
{
	public class UsersTest : BaseTest
	{
		[Test]
		public void GetByIdTest()
		{
			Container.BeginLifetimeScope();
			var userService = Container.Resolve<UserService>();
			var user = userService.GetUserById(1);
			Assert.AreEqual(TestUsers.User1, user);
		}

		[Test]
		public void GetUsersTest()
		{
			Container.BeginLifetimeScope();
			var userService = Container.Resolve<UserService>();
			var users = userService.GetUsers();
			Assert.AreEqual(2, users.Count);
		}
		
		[Test]
		public void RegistrationTest()
		{
			Container.BeginLifetimeScope();
			var @event = Container.Resolve<UserRegistered>();
			var eventService = Container.Resolve<TestEventService>();
			var userService = Container.Resolve<UserService>();

			var userCount = userService.GetUsers().Count;
			userService.RegisterUser(u =>
			{
				u.Nickname = "test";
			});
			Assert.True(eventService.IsRaised(@event));
			
			Assert.Less(userCount, userService.GetUsers().Count);
		}
	}
}