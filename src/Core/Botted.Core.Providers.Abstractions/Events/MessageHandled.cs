using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Providers.Abstractions.Data;

namespace Botted.Core.Providers.Abstractions.Events
{
	public class MessageHandled : EventWithData<Message> { }
}