using Botted.Core.Abstractions.Services.Providers;

namespace Botted.Core.Abstractions.Data
{
	public class BotMessage
	{
		public ProviderIdentifier Provider { get; init; }
		public string Text { get; init; }
		public long UserId { get; init; }
	}
}