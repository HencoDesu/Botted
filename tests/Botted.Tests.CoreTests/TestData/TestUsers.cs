using Botted.Core.Users.Abstractions.Data;

namespace Botted.Tests.CoreTests.TestData
{
	public static class TestUsers
	{
		public static BottedUser TestBottedUser { get; } = new() { Id = 0, Nickname = "TestUser"};
		public static BottedUser HencoDesu { get; } = new() { Id = 8, Nickname = "HencoDesu" };
	}
}