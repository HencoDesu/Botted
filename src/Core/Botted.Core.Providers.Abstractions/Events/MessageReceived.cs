using System.Diagnostics.CodeAnalysis;
using Botted.Core.Events.Abstractions;

namespace Botted.Core.Providers.Abstractions.Events
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
	public class MessageReceived : IEventWithData<Data.Message> { }
}