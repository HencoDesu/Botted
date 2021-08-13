using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Providers;

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
			=> OnMessageReceived(new BotMessage
			{
				Provider = ProviderIdentifier.Any,
				Text = message
			});
	}
}