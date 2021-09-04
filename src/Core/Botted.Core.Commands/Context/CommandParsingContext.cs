using System.Collections.Generic;
using Botted.Core.Commands.Abstractions.Context;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Commands.Context
{
	/// <inheritdoc />
	public class CommandParsingContext : ICommandParsingContext
	{
		public CommandParsingContext(IReadOnlyList<string> textArgs, User? user)
		{
			TextArgs = textArgs;
			User = user;
		}

		public IReadOnlyList<string> TextArgs { get; }

		public User? User { get; }
	}
}