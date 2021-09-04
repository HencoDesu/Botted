using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Context;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Commands.Abstractions.Events;
using Botted.Core.Commands.Abstractions.Result;
using Botted.Core.Commands.Context;
using Botted.Core.Events.Abstractions;
using Botted.Core.Providers.Abstractions.Data;
using Botted.Core.Providers.Abstractions.Events;

namespace Botted.Core.Commands
{
	/// <inheritdoc />
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
	public class CommandService : ICommandService
	{
		private readonly Dictionary<string, Func<ICommandData, Task<ICommandResult>>> _handlers = new();
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

		public void RegisterCommand<TData>(ICommand<TData> command) 
			where TData : class, ICommandData
		{
			if (_handlers.ContainsKey(command.Name) || _dataStructures.ContainsKey(command.Name)) {
				// TODO: Custom exception here
				throw new Exception();
			}
			
			_handlers.Add(command.Name, data => command.Execute((data as TData)!));
			_dataStructures.Add(command.Name, TData.Structure);
		}
		
		private async Task OnMessageReceived(Message message)
		{
			if (!_parser.TryParseCommandName(message, out var commandName))
			{
				return;
			}
			
			_handlers.TryGetValue(commandName, out var handler);
			_dataStructures.TryGetValue(commandName, out var dataStructure);

			if (handler is null || dataStructure is null)
			{
				// TODO: Custom exception here
				throw new Exception();
			}

			var data = _parser.ParseCommandData(message, dataStructure);
			var executingContext = new CommandExecutingContext(commandName, data, true);
			_eventService.Raise<CommandExecuting, ICommandExecutingContext>(executingContext);
			if (executingContext.CanExecute)
			{
				var result = await handler(data);
				_eventService.Raise<MessageHandled, Message>(message with { Text = result.Text });
			}
		}
	}
}