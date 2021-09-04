using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Providers.Abstractions.Data
{
	public record Message()
	{
		public Message(string text)
			: this(text, ProviderIdentifier.Any, null)
		{ }

		public Message(string text,
					   ProviderIdentifier provider,
					   User? user)
			: this()
		{
			Text = text;
			Provider = provider;
			User = user;
		}

		public string Text { get; init; }
		public ProviderIdentifier Provider { get; }

		public User? User { get; }
	}
}