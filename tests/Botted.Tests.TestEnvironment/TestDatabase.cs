using Botted.Core.Abstractions.Services.Users.Data;
using Botted.Core.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace Botted.Tests.TestEnvironment
{
	public class TestDatabase : BotDatabase
	{
		public TestDatabase(params BotUser[] users)
			: base(new DbContextOptionsBuilder<BotDatabase>()
				   .UseInMemoryDatabase("BotDatabase")
				   .Options)
		{
			foreach (var user in users)
			{
				Users.Add(user);
			}

			SaveChanges();
		}
	}
}