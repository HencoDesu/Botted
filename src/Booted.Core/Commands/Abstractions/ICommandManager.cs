using Booted.Core.Providers;

namespace Booted.Core.Commands.Abstractions
{
	public interface ICommandManager
	{
		public void RegisterCommand(ICommandBase commandBase);

		public ICommandResult TryExecuteCommand(BotMessage message);
	}
}