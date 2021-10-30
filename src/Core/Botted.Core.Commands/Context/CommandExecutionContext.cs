using Botted.Core.Commands.Abstractions.Context;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Messaging.Data;

namespace Botted.Core.Commands.Context
{
	/// <inheritdoc />
	public class CommandExecutionContext : ICommandExecutionContext
	{
		public CommandExecutionContext(string commandName, 
									   ICommandData commandData,
									   BottedMessage message,
									   bool canExecute)
		{
			CommandName = commandName;
			CommandData = commandData;
			CanExecute = canExecute;
			Message = message;
		}

		public string CommandName { get; }

		public ICommandData CommandData { get; }

		public bool CanExecute { get; set; }
		
		public BottedMessage Message { get; }
	}
}