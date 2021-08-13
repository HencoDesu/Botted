using System.Collections.Generic;
using System.Linq;
using Botted.Core.Abstractions.Bot;
using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Providers;
using Pidgin;
using Parser = Booted.Core.Services.Commands.Parser;

namespace Booted.Core.Bot
{
	public class Bot : IBot
	{
		private readonly IEnumerable<ICommand> _commands;
		
		public IEventService EventService { get; }

		public Bot(IEventService eventService, IEnumerable<ICommand> commands, IEnumerable<IProviderService> providers)
		{
			_commands = commands;
			EventService = eventService;
			EventService.Subscribe<MessageReceived, BotMessage>(OnMessageReceived);
		}

		private void OnMessageReceived(BotMessage message)
		{
			var commandName = Parser.CommandName.ParseOrThrow(message.Text);
			var command = _commands.FirstOrDefault(c => c.Name == commandName);
			
			if (command is null || !command.ProviderLimitation.IsMatching(message.Provider))
			{
				return;
			} 
			
			var data = command.Structure.ParseData(message);
			var result = command.Execute(data);
			OnCommandExecuted(result, message);
		}

		private void OnCommandExecuted(ICommandResult commandResult, BotMessage inputMessage)
		{
			var finalMessage = new BotMessage
			{
				Provider = inputMessage.Provider,
				Text = commandResult.Text,
				UserId = inputMessage.UserId
			};
			EventService.Raise<NeedSendMessage, BotMessage>(finalMessage);
		}
	}
}