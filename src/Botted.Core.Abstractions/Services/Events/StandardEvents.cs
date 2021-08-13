using Botted.Core.Abstractions.Data;

namespace Botted.Core.Abstractions.Services.Events
{
	public sealed class MessageReceived : IEvent<BotMessage> { }
	public sealed class NeedSendMessage : IEvent<BotMessage> { }
}