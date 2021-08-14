using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Providers;
using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Tests.TestEnvironment
{
	public class TestProvider : ProviderService
	{
		public TestProvider(IEventService eventService) 
			: base(eventService, ProviderIdentifier.Any)
		{ }

		public BotMessage? LastSentMessage { get; private set; }

		public override void SendMessage(BotMessage message)
			=> LastSentMessage = message;

		public void ReceiveMessage(string message) 
			=> ReceiveMessage(message, new BotUser());

		public void ReceiveMessage(string message, BotUser user)
			=> OnMessageReceived(new BotMessage
			{
				Provider = ProviderIdentifier.Any,
				Text = message,
				User = user
			});
	}
}