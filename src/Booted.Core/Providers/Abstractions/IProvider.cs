using Booted.Core.Events.EventData;

namespace Booted.Core.Providers.Abstractions
{
	public interface IProvider
	{
		void SendMessage(BotMessage message);
	}
}