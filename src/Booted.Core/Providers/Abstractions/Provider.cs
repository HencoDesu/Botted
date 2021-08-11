using Booted.Core.Events;
using Booted.Core.Events.Abstractions;
using Booted.Core.Events.EventData;

namespace Booted.Core.Providers.Abstractions
{
	public abstract class Provider : IProvider
	{
		private readonly ProviderIdentifier _identifier;
		
		protected Provider(IEventService eventService, 
						   ProviderIdentifier identifier)
		{
			EventService = eventService;
			_identifier = identifier;
			
			EventService.Subscribe<NeedSendMessage, BotMessage>(OnNeedSendMessage);
		}
		
		protected IEventService EventService { get; }

		public abstract void SendMessage(BotMessage message);

		protected void OnMessageReceived(BotMessage message)
		{
			EventService.Raise<MessageReceived, BotMessage>(message);
		}
		
		protected void OnNeedSendMessage(BotMessage message)
		{
			if (message.Provider.IsMatching(_identifier))
			{
				SendMessage(message);
			}
		}
	}
}