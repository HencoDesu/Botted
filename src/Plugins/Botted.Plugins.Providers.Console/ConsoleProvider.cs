using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Providers.Abstractions;
using Botted.Core.Providers.Abstractions.Data;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Plugins.Providers.Console
{
	public class ConsoleProvider : AbstractProviderService
	{
		public static ProviderIdentifier Identifier { get; } = new();

		public ConsoleProvider(IEventService eventService)
			: base(eventService, Identifier)
		{
			eventService.Subscribe<BotStarted>(OnBotStarted);
		}

		private void OnBotStarted()
		{
			while (true)
			{
				WaitForInput();
			}
		}

		public override void SendMessage(Message message)
		{
			System.Console.WriteLine(message.Text);
		}

		public void WaitForInput()
		{
			var message = System.Console.ReadLine();
			OnMessageReceived(new Message(message!, Identifier, new User()));
		}
	}
}