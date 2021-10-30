using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Messaging.Data
{
	public class BottedMessage
	{
		public long? ChatId { get; set; }
		public ProviderIdentifier? ProviderIdentifier { get; init; }
		public string? Text { get; init; }
		public BottedUser? Sender { get; init; }
	}
}