using Botted.Core.Abstractions.Services.Providers;
using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Core.Abstractions.Data
{
	public record BotMessage
	{
		public ProviderIdentifier Provider { get; init; }
		public string Text { get; init; }
		public BotUser User { get; init; }
	}
}