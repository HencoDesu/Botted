using System;
using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Services.Database;
using Botted.Tests.TestEnvironment.Data;
using Microsoft.EntityFrameworkCore;

namespace Botted.Tests.TestEnvironment
{
	public class TestDatabase : BotDatabase
	{
		public TestDatabase()
			: base(new DbContextOptionsBuilder<BotDatabase>()
				   .UseInMemoryDatabase(Guid.NewGuid().ToString())
				   .Options)
		{
			Users.Add(TestUsers.User1);
			Users.Add(TestUsers.User2);

			SaveChanges();
		}
	}
}