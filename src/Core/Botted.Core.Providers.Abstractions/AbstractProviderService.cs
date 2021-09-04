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

			EventService.Subscribe<MessageHandled, Message>(OnMessageHandled);
		}
		
		protected IEventService EventService { get; }

		public abstract void SendMessage(Message message);

		protected void OnMessageReceived(Message message)
		{
			EventService.Raise<MessageReceived, Message>(message);
		}
		
		protected void OnMessageHandled(Message message)
		{
			if (message.Provider.IsMatching(_identifier))
			{
				SendMessage(message);
			}
		}
	}
}