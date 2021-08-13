using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Providers;

namespace Botted.Providers.Console
{
	public class ConsoleProvider : ProviderService
	{
		public static ProviderIdentifier Identifier { get; } = new();
		
		public ConsoleProvider(IEventService eventService) : base(eventService, Identifier)
		{ }

		public override void SendMessage(BotMessage message)
		{
			System.Console.WriteLine(message.Text);
		}
	}
}