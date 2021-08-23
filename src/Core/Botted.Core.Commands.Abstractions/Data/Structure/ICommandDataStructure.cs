using System.Collections.Generic;

namespace Botted.Core.Commands.Abstractions.Data.Structure
{
	/// <summary>
	/// Basic structure for command
	/// </summary>
	public interface ICommandDataStructure
	{
		/// <summary>
		/// Command arguments
		/// </summary>
		IReadOnlyCollection<IArgumentStructure> Arguments { get; }
		
		/// <summary>
		/// Returns empty instance of <see cref="ICommandData"/> <br/>
		/// This instance will filled during parsing
		/// </summary>
		ICommandData Empty { get; }
	}
}