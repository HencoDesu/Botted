using Botted.Core.Abstractions.Data;

namespace Botted.Core.Abstractions.Services.Providers
{
	public interface IProviderService : IService
	{
		void SendMessage(BotMessage message);
	}
}