using System.Collections.Generic;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Commands
{
	/// <inheritdoc />
	public class CommandParsingContext : ICommandParsingContext
	{
		public CommandParsingContext(IReadOnlyList<string> textArgs, User user)
		{
			TextArgs = textArgs;
			User = user;
		}

		public IReadOnlyList<string> TextArgs { get; }

		public User User { get; }
	}
}