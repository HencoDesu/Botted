using Botted.Core.Abstractions.Services.Providers;

namespace Botted.Core.Abstractions.Data
{
	public class BotMessage
	{
		public ProviderIdentifier Provider { get; set; }
		public string Text { get; set; }
		public long UserId { get; set; }
	}
}