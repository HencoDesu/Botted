using System.Diagnostics.CodeAnalysis;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Providers.Abstractions.Data;

namespace Botted.Core.Providers.Abstractions.Events
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
	public class MessageReceived : EventWithData<Message> { }
}