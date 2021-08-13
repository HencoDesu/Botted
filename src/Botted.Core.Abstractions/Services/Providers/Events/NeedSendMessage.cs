using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Services.Events;

namespace Botted.Core.Abstractions.Services.Providers.Events
{
	public sealed class NeedSendMessage : IEvent<BotMessage> { }
}