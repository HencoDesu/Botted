using System.Threading.Tasks;
using Botted.Core.Events.Abstractions;
using Botted.Core.Providers.Abstractions.Data;
using Botted.Core.Providers.Abstractions.Events;

namespace Botted.Core.Providers.Abstractions
{
	/// <inheritdoc />
	public abstract class AbstractProviderService : IProviderService
	{
		private readonly ProviderIdentifier _identifier;
		
		protected AbstractProviderService(IEventService eventService, 
										  ProviderIdentifier identifier)
		{
			_identifier = identifier;
			EventService = eventService;

			EventService.GetEvent<MessageHandled>()
						.Subscribe(OnMessageHandled);
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
	}
}