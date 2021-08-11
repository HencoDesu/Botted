using Booted.Core.Events.Abstractions;
using Booted.Core.Events.EventData;

namespace Booted.Core.Events
{
	public sealed class MessageReceived : IEvent<BotMessage> { }
	public sealed class BotStateChanged : IEvent<BotState> {}
	public sealed class NeedSendMessage : IEvent<BotMessage> { }
}