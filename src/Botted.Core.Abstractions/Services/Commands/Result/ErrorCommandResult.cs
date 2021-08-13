namespace Botted.Core.Abstractions.Services.Commands.Result
{
	public class ErrorCommandResult : ICommandResult
	{
		public ErrorCommandResult(string text)
		{
			Text = text;
		}

		public string Text { get; }
	}
}