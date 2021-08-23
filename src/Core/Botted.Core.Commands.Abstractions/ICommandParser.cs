using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Providers.Abstractions;

namespace Botted.Core.Commands.Abstractions
{
	/// <summary>
	/// Represents a parser for the command
	/// </summary>
	public interface ICommandParser
	{
		/// <summary>
		/// Parses a command name from a <see cref="Message"/>
		/// </summary>
		/// <param name="message">Message to parse</param>
		/// <param name="commandName">Parsed command name</param>
		/// <returns>Success flag</returns>
		bool TryParseCommandName(Message message, out string commandName);
		
		/// <summary>
		/// Parses a command data from a <see cref="Message"/> using provided <see cref="ICommandDataStructure"/>
		/// </summary>
		/// <param name="message">Message structure</param>
		/// <param name="structure">Structure to parse</param>
		/// <returns>Parsed command data</returns>
		ICommandData ParseCommandData(Message message, ICommandDataStructure structure);
	}
}