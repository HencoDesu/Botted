using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Messaging.Data;
using JetBrains.Annotations;

namespace Botted.Core.Messaging.Events
{
	[UsedImplicitly]
	public class MessageReceived : EventWithData<BottedMessage>
	{ }
}