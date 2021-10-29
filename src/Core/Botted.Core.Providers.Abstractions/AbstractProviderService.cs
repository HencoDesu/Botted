using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Providers.Abstractions.Data;
using Botted.Core.Providers.Abstractions.Events;

namespace Botted.Core.Providers.Abstractions
{
	/// <inheritdoc />
	public abstract partial class AbstractProviderService : IProviderService
	{
		private readonly ProviderIdentifier _identifier;

		protected CancellationTokenSource Cts { get; } = new();
		
		protected AbstractProviderService(IEventService eventService, 
										  ProviderIdentifier identifier)
		{
			_identifier = identifier;
			EventService = eventService;
			
			eventService.GetEvent<MessageHandled>().Subscribe(OnMessageHandled);
			eventService.GetEvent<BotStarted>().Subscribe(OnBotStarted);
			eventService.GetEvent<BotStopped>().Subscribe(OnBotStopped);
		}
		
		protected IEventService EventService { get; }

		public abstract Task SendMessage(Message message);

		protected void OnMessageReceived(Message message)
		{
			EventService.GetEvent<MessageReceived>().Raise(message);
		}

		protected async Task OnMessageHandled(Message message)
		{
			if (message.Provider.IsMatching(_identifier))
			{
				await SendMessage(message);
			}
		}

		protected virtual void OnBotStarted()
		{
			var token = Cts.Token;

			Task.Run(() => WaitForUpdates(token), token);
		}

		protected void OnBotStopped()
		{
			Cts.Cancel();
		}

		protected virtual void WaitForUpdates(CancellationToken cancellationToken)
		{
			
		}
	}
}