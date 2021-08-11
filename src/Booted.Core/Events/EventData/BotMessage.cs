using Booted.Core.Providers;

namespace Booted.Core.Events.EventData
{
	public class BotMessage
	{
		public ProviderIdentifier Provider { get; set; }
		public string Text { get; set; }
		public long UserId { get; set; }
	}
}