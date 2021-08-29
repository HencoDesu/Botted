using System.Collections.Generic;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Commands.Abstractions.Context
{
	/// <summary>
	/// Context of executed command
	/// </summary>
	public interface ICommandParsingContext
	{
		/// <summary>
		/// Raw text args for command
		/// </summary>
		IReadOnlyList<string> TextArgs { get; }
		
		/// <summary>
		/// User executed command
		/// </summary>
		User User { get; }
	}
}