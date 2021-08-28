using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Providers.Abstractions.Data
{
	public record Message()
	{
		public string Text { get; init; }
		public ProviderIdentifier Provider { get; init; }

		public User User { get; set; }
	}
}