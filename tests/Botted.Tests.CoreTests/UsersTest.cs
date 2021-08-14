using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Abstractions.Services.Users.Events;
using Botted.Core.Services.Users;
using Botted.Tests.TestEnvironment;
using FakeItEasy;
using NUnit.Framework;

namespace Botted.Tests.CoreTests
{
	public class UsersTest
	{
		private BotUser TestUser1 { get; }
			= new ()
			{
				Id = 1,
				Nickname = "test"
			};
		
		private BotUser TestUser2 { get; }
			= new ()
			{
				Id = 2,
				Nickname = "test"
			};
		
		[Test]
		public void GetByIdTest()
		{
			var database = new TestDatabase(TestUser1);
			var userService = new UserService(database, A.Fake<IEventService>());
			var user = userService.GetUserById(1);
			Assert.AreEqual(TestUser1, user);
		}

		[Test]
		public void GetUsersTest()
		{
			var database = new TestDatabase(TestUser1, TestUser2);
			var userService = new UserService(database, A.Fake<IEventService>());
			var users = userService.GetUsers();
			Assert.AreEqual(2, users.Count);
		}
		
		[Test]
		public void RegistrationTest()
		{
			var @event = new UserRegistered();
			var eventService = new TestEventService(@event);
			var database = new TestDatabase();
			var userService = new UserService(database, eventService);
			
			var users = userService.GetUsers();
			Assert.AreEqual(0, users.Count);
			
			userService.RegisterUser(u =>
			{
				u.Id = TestUser1.Id;
				u.Nickname = TestUser1.Nickname;
			});
			Assert.True(eventService.IsRaised(@event));
			
			var user = userService.GetUserById(TestUser1.Id);
			Assert.AreEqual(TestUser1.Nickname, user.Nickname);
			
			users = userService.GetUsers();
			Assert.AreEqual(1, users.Count);
		}
	}
}