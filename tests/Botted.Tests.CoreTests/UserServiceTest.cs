using System.Collections.Generic;
using System.Linq;
using Botted.Core.Events.Abstractions;
using Botted.Core.Users;
using Botted.Core.Users.Abstractions;
using Botted.Core.Users.Abstractions.Data;
using Botted.Tests.CoreTests.TestData;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Botted.Tests.CoreTests
{
	public class UserServiceTest
	{
		private readonly IUserDatabase _userDatabase;

		public UserServiceTest()
		{
			var usersList = new List<User> { TestUsers.TestUser, TestUsers.HencoDesu };
			_userDatabase = A.Fake<IUserDatabase>();
			A.CallTo(() => _userDatabase.Users)
			 .Returns(new EnumerableQuery<User>(usersList));
		}
		
		[Fact]
		public void RegistrationTest()
		{
			var userDatabase = A.Fake<IUserDatabase>();
			var userService = new UserService(userDatabase, A.Fake<IEventService>());
			
			userService.Register();
			
			A.CallTo(() => userDatabase.RegisterUser()).MustHaveHappenedOnceExactly();
		}

		[Fact]
		public void GetByIdTest()
		{
			var userService = new UserService(_userDatabase, A.Fake<IEventService>());
			
			var user = userService.GetById(8);

			user.Should().Be(TestUsers.HencoDesu);
		}

		[Fact]
		public void GetAllTest()
		{
			var userService = new UserService(_userDatabase, A.Fake<IEventService>());
			
			var users = userService.GetAll();

			users.Count.Should().Be(2);
		}

		[Fact]
		public void GetByPredicateTest()
		{
			var userService = new UserService(_userDatabase, A.Fake<IEventService>());
			
			var users = userService.GetAll(u => u.Nickname == "HencoDesu");

			users.Count.Should().Be(1);
		}
	}
}