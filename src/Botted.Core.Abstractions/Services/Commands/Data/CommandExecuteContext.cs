using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Core.Abstractions.Services.Commands.Data
{
	public class CommandExecuteContext
	{
		public CommandExecuteContext(ICommand command, 
									 BotUser user, 
									 bool canExecute, 
									 ICommandData? data)
		{
			Command = command;
			User = user;
			CanExecute = canExecute;
			CommandData = data;
		}

		public ICommand Command { get; }
		public BotUser User { get; }
		public bool CanExecute { get; set; }
		
		public ICommandData? CommandData { get; }
	}
}