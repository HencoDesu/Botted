namespace Botted.Core.Commands.Abstractions.Result
{
	/// <summary>
	/// Represents that some ERROR occured during command execution
	/// </summary>
	public class ErrorCommandResult : ICommandResult
	{
		/// <summary>
		/// Initialize new instance of <see cref="ErrorCommandResult"/>
		/// </summary>
		/// <param name="text">Text that describes error</param>
		public ErrorCommandResult(string text)
		{
			Text = text;
		}

		/// <inheritdoc />
		public string Text { get; }
	}
}