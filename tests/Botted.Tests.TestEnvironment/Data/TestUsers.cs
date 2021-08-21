using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Tests.TestEnvironment.Data
{
	public static class TestUsers
	{
		public static BotUser User1 { get; }
			= new (1, "test1");
		
		public static BotUser User2 { get; }
			= new (2, "test2");
	}
}