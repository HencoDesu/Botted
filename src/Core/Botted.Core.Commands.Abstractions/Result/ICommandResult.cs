namespace Botted.Core.Commands.Abstractions.Result
{
	/// <summary>
	/// Represents a command execution result
	/// </summary>
	public interface ICommandResult
	{
		/// <summary>
		/// Text that will be used in result message
		/// </summary>
		string Text { get; }
	}
}