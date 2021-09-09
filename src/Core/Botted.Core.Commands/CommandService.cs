using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Context;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Commands.Abstractions.Events;
using Botted.Core.Commands.Context;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Extensions;
using Botted.Core.Providers.Abstractions.Data;
using Botted.Core.Providers.Abstractions.Events;
using JetBrains.Annotations;

namespace Botted.Core.Commands
{
	/// <inheritdoc />
	[UsedImplicitly]
	public class CommandService : ICommandService
	{
		private readonly Subject<Message> _messageSubject = new();
		private readonly Subject<ICommandExecutionContext> _executionSubject = new();
		private readonly Dictionary<string, ICommandDataStructure> _dataStructures = new();
		private readonly IEventService _eventService;
		private readonly ICommandParser _parser;

		public CommandService(IEventService eventService, 
							  ICommandParser parser)
		{
			_eventService = eventService;
			_parser = parser;
			
			_eventService.GetEvent<MessageReceived>()
						 .SafeSubscribe(OnMessageReceived);

			_messageSubject.Select(ParseContext)
						   .WhereNotNull()
						   .Subscribe(context =>
						   {
							   _eventService.Raise<CommandExecuting, ICommandExecutionContext>(context);
							   _executionSubject.OnNext(context);
						   });
		}

		public void RegisterCommand<TData>(ICommand<TData> command) 
			where TData : class, ICommandData
		{
			if (_dataStructures.ContainsKey(command.Name)) {
				// TODO: Custom exception here
				throw new Exception();
			}
			
			_dataStructures.Add(command.Name, TData.Structure);
			_executionSubject.Where(context => context.CommandName == command.Name)
							 .Where(context => context.CanExecute)
							 .Select(context => context.CommandData)
							 .SubscribeAsync(data => ExecuteCommand(command, (data as TData)!));
		}
		
		private void OnMessageReceived(Message message)
		{
			_messageSubject.OnNext(message);
		}

		private ICommandExecutionContext? ParseContext(Message message)
		{
			if (!_parser.TryParseCommandName(message, out var commandName))
			{
				return null;
			}

			if (!_dataStructures.ContainsKey(commandName))
			{
				//TODO: Custom exception here
				throw new Exception();
			}

			var structure = _dataStructures[commandName];
			var data = _parser.ParseCommandData(message, structure);
			return new CommandExecutionContext(commandName, data, true);
		}
		
		private async Task ExecuteCommand<TCommandData>(ICommand<TCommandData> command, TCommandData data) 
			where TCommandData : class, ICommandData
		{
			var result = await command.Execute(data);
			var message = new Message(result.Text);
			_eventService.Raise<MessageHandled, Message>(message);
		}
	}
}