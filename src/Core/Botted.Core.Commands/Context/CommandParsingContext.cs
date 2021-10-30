﻿using System.Collections.Generic;
using Botted.Core.Commands.Abstractions.Context;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Commands.Context
{
	/// <inheritdoc />
	public class CommandParsingContext : ICommandParsingContext
	{
		public CommandParsingContext(IReadOnlyList<string> textArgs, BottedUser? user)
		{
			TextArgs = textArgs;
			User = user;
		}

		public IReadOnlyList<string> TextArgs { get; }

		public BottedUser? User { get; }
	}
}