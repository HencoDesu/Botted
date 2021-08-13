using System.Diagnostics.CodeAnalysis;
using Botted.Core.Abstractions.Data;

namespace Botted.Core.Abstractions.Services.Providers
{
	[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
	public interface IProviderService : IService
	{
		void SendMessage(BotMessage message);
	}
}