using System.Collections.Generic;
using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Core.Abstractions.Services.Commands.Data
{
	public class CommandParsingContext
	{
		public CommandParsingContext(BotUser user, 
									 IReadOnlyList<string> textArgs)
		{
			User = user;
			TextArgs = textArgs;
		}

		public BotUser User { get; }
		public IReadOnlyList<string> TextArgs { get; }
	}
}