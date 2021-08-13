namespace Botted.Core.Abstractions.Services.Commands.Result
{
	public class OkCommandResult : ICommandResult
	{
		public OkCommandResult(string text)
		{
			Text = text;
		}

		public string Text { get; }
	}
}