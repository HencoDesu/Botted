namespace Botted.Core.Commands.Abstractions.Result
{
	/// <summary>
	/// Represents a OK result 
	/// </summary>
	public class OkCommandResult : ICommandResult
	{
		/// <summary>
		/// Initialize new instance of <see cref="OkCommandResult"/>
		/// </summary>
		/// <param name="text">Text that will be sent to user as response</param>
		public OkCommandResult(string text)
		{
			Text = text;
		}

		/// <inheritdoc />
		public string Text { get; }
	}
}