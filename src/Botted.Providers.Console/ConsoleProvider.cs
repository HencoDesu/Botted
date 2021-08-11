using Booted.Core.Dependencies.Attributes;
using Booted.Core.Events.Abstractions;
using Booted.Core.Events.EventData;
using Booted.Core.Providers;
using Booted.Core.Providers.Abstractions;

namespace Botted.Providers.Console
{
	[Service]
	public class ConsoleProvider : Provider
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