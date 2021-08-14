using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Events;
using NLog;
using Pidgin;

namespace Botted.Core.Services.Commands
{
	public class CommandService : ICommandService
	{
		private readonly IEventService _eventService;
		private readonly IEnumerable<ICommand> _commands;
		private readonly ICommandResultFactory _commandResultFactory;
		private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

		public CommandService(IEventService eventService, 
							  IEnumerable<ICommand> commands, 
							  ICommandResultFactory commandResultFactory)
		{
			_eventService = eventService;
			_commands = commands;
			_commandResultFactory = commandResultFactory;

			_eventService.Subscribe<MessageReceived, BotMessage>(OnMessageReceived);
		}
		
		private void OnMessageReceived(BotMessage message)
		{
			try
			{
				var commandName = Parser.CommandName.ParseOrThrow(message.Text);
				var command = _commands.FirstOrDefault(c => c.Name == commandName);
			
				if (command is null || !command.ProviderLimitation.IsMatching(message.Provider))
				{
					return;
				} 
			
				var data = command.Structure?.ParseData(message);
				var result = command.Execute(data);
				OnCommandExecuted(result, message);
			} catch (Exception e)
			{
				_logger.Error(e, "Error on handling message: {0}", message.Text);
				OnCommandExecuted(_commandResultFactory.Error(e), message);
			}
		}
		
		private void OnCommandExecuted(ICommandResult commandResult, BotMessage inputMessage)
		{
			var finalMessage = new BotMessage
			{
				Provider = inputMessage.Provider,
				Text = commandResult.Text,
				UserId = inputMessage.UserId
			};
			_eventService.Rise<NeedSendMessage, BotMessage>(finalMessage);
		}
	}
}