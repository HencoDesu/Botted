using Botted.Core.Commands.Abstractions.Data;

namespace Botted.Core.Commands.Abstractions.Context
{
	/// <summary>
	/// Represents a simple context of executing command
	/// </summary>
	public interface ICommandExecutingContext
	{
		/// <summary>
		/// Name of executing command
		/// </summary>
		string CommandName { get; }
		
		/// <summary>
		/// Data for that execution
		/// </summary>
		ICommandData CommandData { get; }
		
		/// <summary>
		/// Can that command be executed
		/// </summary>
		bool CanExecute { get; set; }
	}
}