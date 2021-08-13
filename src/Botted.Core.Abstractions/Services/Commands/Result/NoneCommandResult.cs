namespace Botted.Core.Abstractions.Services.Commands.Result
{
	public class NoneCommandResult : ICommandResult
	{
		public string Text => string.Empty;
	}
}