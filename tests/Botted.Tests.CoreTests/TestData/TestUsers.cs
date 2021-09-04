using Botted.Core.Users.Abstractions.Data;

namespace Botted.Tests.CoreTests.TestData
{
	public class TestUsers : User
	{
		public static User TestUser { get; } = new() { Id = 0, Nickname = "TestUser"};
		public static User HencoDesu { get; } = new() { Id = 8, Nickname = "HencoDesu" };
	}
}