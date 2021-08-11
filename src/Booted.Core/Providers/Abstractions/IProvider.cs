using System;

namespace Booted.Core.Providers
{
	public interface IProvider
	{
		event Action<IProvider, BotMessage> MessageReceived;

		void SendMessage(BotMessage message);
	}
}