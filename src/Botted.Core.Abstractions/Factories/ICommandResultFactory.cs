using Botted.Core.Abstractions.Services.Commands;

namespace Botted.Core.Abstractions.Factories
{
	public interface ICommandResultFactory
	{
		ICommandResult Ok(string text);
		ICommandResult Error(string text);
	}
}