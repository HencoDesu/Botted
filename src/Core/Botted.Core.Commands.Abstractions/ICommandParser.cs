using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Messaging.Data;

namespace Botted.Core.Commands.Abstractions
{
	/// <summary>
	/// Represents a parser for the command
	/// </summary>
	public interface ICommandParser
	{
		/// <summary>
		/// Parses a command name from a <see cref="BottedMessage"/>
		/// </summary>
		/// <param name="message">Message to parse</param>
		/// <param name="commandName">Parsed command name</param>
		/// <returns>Success flag</returns>
		bool TryParseCommandName(BottedMessage message, out string commandName);
		
		/// <summary>
		/// Parses a command data from a <see cref="BottedMessage"/> using provided <see cref="ICommandDataStructure"/>
		/// </summary>
		/// <param name="message">Message structure</param>
		/// <param name="structure">Structure to parse</param>
		/// <returns>Parsed command data</returns>
		ICommandData ParseCommandData(BottedMessage message, ICommandDataStructure structure);
	}
}