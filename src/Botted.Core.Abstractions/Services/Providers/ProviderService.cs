using System.Diagnostics.CodeAnalysis;
using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Providers.Events;

namespace Botted.Core.Abstractions.Services.Providers
{
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public abstract class ProviderService : IProviderService
	{
		private readonly ProviderIdentifier _identifier;
		
		protected ProviderService(IEventService eventService, 
								  ProviderIdentifier identifier)
		{
			_identifier = identifier;
			EventService = eventService;

			EventService.Subscribe<NeedSendMessage, BotMessage>(OnNeedSendMessage);
		}
		
		protected IEventService EventService { get; }

		public abstract void SendMessage(BotMessage message);

		protected void OnMessageReceived(BotMessage message)
		{
			EventService.Rise<MessageReceived, BotMessage>(message);
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