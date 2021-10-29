using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions;
using Botted.Core.Providers.Abstractions;
using Botted.Core.Providers.Abstractions.Data;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Plugins.Providers.Console
{
	public  class ConsoleProvider : AbstractProviderService
	{
		public static ProviderIdentifier Identifier { get; } = new();

		public ConsoleProvider(IEventService eventService)
			: base(eventService, Identifier)
		{ }

		public override Task SendMessage(Message message)
		{
			System.Console.WriteLine(message.Text);
			
			return Task.CompletedTask;
		}

		protected override void WaitForUpdates(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				var message = System.Console.ReadLine();
				OnMessageReceived(new Message(message!, Identifier, new User()));
			}
		}
	}
}