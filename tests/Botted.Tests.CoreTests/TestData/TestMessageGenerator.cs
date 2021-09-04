using Botted.Core.Providers.Abstractions.Data;

namespace Botted.Tests.CoreTests.TestData
{
	public static class TestMessageGenerator
	{
		public static Message GenerateMessage(string text)
		{
			return new Message(text, ProviderIdentifier.Any, TestUsers.TestUser);
		}
	}
}