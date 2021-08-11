using System;
using System.Collections.Generic;
using System.Linq;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Extensions;
using Booted.Core.Providers;
using Pidgin;

namespace Booted.Core.Commands
{
	public class CommandManager : ICommandManager
	{
		private readonly List<ICommandBase> _commands = new();

		public void RegisterCommand(ICommandBase commandBase)
		{
			if (_commands.Contains(commandBase))
			{
				throw new Exception("Command with that name already registered");
			}

			_commands.Add(commandBase);
		}

		public ICommandResult TryExecuteCommand(BotMessage message)
		{
			var commandName = Parser.CommandName.ParseOrThrow(message.Text);
			var commandBase = _commands.FirstOrDefault(c => c.Name == commandName);
			
			if (commandBase is null || !commandBase.AllowedProvider.IsMatching(message.Provider))
			{
				return CommandResult.None();
			}

			try
			{
				if (commandBase is ICommand<Unit> simpleCommand)
				{
					return simpleCommand.Execute(Unit.Value);
				}
			
				var commandType = commandBase.GetType();
				if (commandType.InheritedFrom(typeof(ICommand<>)))
				{
					var parser = commandType.GetPropertyValue<Parser<char, object>>(commandBase, "DataParser")!;
					var data = parser.ParseOrThrow(message.Text);
					var result = commandType.InvokeMethod<ICommandResult>(commandBase, "Execute", data)!;
					return result;
				}
			} catch (Exception e)
			{
				return CommandResult.Error(e.Message);
			}

			return CommandResult.None();
		}
	}
}