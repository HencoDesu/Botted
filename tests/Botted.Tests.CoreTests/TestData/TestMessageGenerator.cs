using Botted.Core.Messaging.Data;

namespace Botted.Tests.CoreTests.TestData
{
	public static class TestMessageGenerator
	{
		public static BottedMessage GenerateMessage(string text)
		{
			return new BottedMessage()
			{
				Text = text,
				ChatId = 0,
				ProviderIdentifier = ProviderIdentifier.Any,
				Sender = TestUsers.TestBottedUser
			};
		}
	}
}