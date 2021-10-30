using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions;
using Botted.Core.Messaging.Data;
using Botted.Core.Messaging.Events;

namespace Botted.Core.Messaging.Services
{
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public abstract class AbstractMessageProvider : IMessageProvider
	{
		private readonly ProviderIdentifier _identifier;
		private readonly IEventService _eventService;
		
		protected AbstractMessageProvider(IEventService eventService, 
										  ProviderIdentifier identifier)
		{
			_eventService = eventService;
			_identifier = identifier;

			_eventService.GetEvent<MessageSent>().Subscribe(OnMessageSent);
		}

		public abstract Task SendMessage(BottedMessage message);

		protected void OnMessageReceived(BottedMessage message)
		{
			_eventService.GetEvent<MessageReceived>().Raise(message);
		}

		protected Task OnMessageSent(BottedMessage message)
		{
			if (message.ProviderIdentifier is null 
			  || message.ProviderIdentifier == _identifier)
			{
				return SendMessage(message);
			}
			
			return Task.CompletedTask;
		}
	}
}