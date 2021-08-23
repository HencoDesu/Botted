using Botted.Core.Events.Abstractions;
using Botted.Core.Providers.Abstractions.Events;

namespace Botted.Core.Providers.Abstractions
{
	/// <inheritdoc />
	public abstract class AbstractProviderService : IProviderService
	{
		private readonly Data.ProviderIdentifier _identifier;
		
		protected AbstractProviderService(IEventService eventService, 
										  Data.ProviderIdentifier identifier)
		{
			_identifier = identifier;
			EventService = eventService;

			EventService.Subscribe<MessageHandled, Data.Message>(OnMessageHandled);
		}
		
		protected IEventService EventService { get; }

		public abstract void SendMessage(Data.Message message);

		protected void OnMessageReceived(Data.Message message)
		{
			EventService.Raise<MessageReceived, Data.Message>(message);
		}
		
		protected void OnMessageHandled(Data.Message message)
		{
			if (message.Provider.IsMatching(_identifier))
			{
				SendMessage(message);
			}
		}
	}
}