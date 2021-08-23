using System;
using System.Collections.Generic;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Commands.Abstractions.Result;
using Botted.Core.Events.Abstractions;
using Botted.Core.Providers.Abstractions;
using Botted.Core.Providers.Abstractions.Events;

namespace Botted.Core.Commands
{
	/// <inheritdoc />
	public class CommandService : ICommandService
	{
		private readonly Dictionary<string, Func<ICommandData, ICommandResult>> _handlers = new();
		private readonly Dictionary<string, ICommandDataStructure> _dataStructures = new ();
		private readonly IEventService _eventService;
		private readonly ICommandParser _parser;

		public CommandService(IEventService eventService, 
							  ICommandParser parser)
		{
			_eventService = eventService;
			_parser = parser;
			
			_eventService.Subscribe<MessageReceived, Message>(OnMessageReceived);
		}

		public void RegisterCommand<TData>(ICommand<TData> command) where TData : class, ICommandData
		{
			if (_handlers.ContainsKey(command.Name) || _dataStructures.ContainsKey(command.Name)) {
				throw new Exception(); // TODO: Custom exception here
			}
			
			_handlers.Add(command.Name, data => command.Execute((data as TData)!));
			_dataStructures.Add(command.Name, TData.Structure);
		}
		
		private void OnMessageReceived(Message message)
		{
			if (!_parser.TryParseCommandName(message, out var commandName))
			{
				return;
			}
			
			_handlers.TryGetValue(commandName, out var handler);
			_dataStructures.TryGetValue(commandName, out var dataStructure);

			if (handler is null || dataStructure is null)
			{
				throw new Exception(); // TODO: Custom exception here
			}

			var data = _parser.ParseCommandData(message, dataStructure);
			var result = handler(data);
			_eventService.Raise<MessageHandled, Message>(message with { Text = result.ToString() });
		}
	}
}