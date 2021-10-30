using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Context;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Commands.Abstractions.Events;
using Botted.Core.Commands.Context;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Extensions;
using Botted.Core.Messaging.Data;
using Botted.Core.Messaging.Events;
using JetBrains.Annotations;

namespace Botted.Core.Commands
{
	/// <inheritdoc />
	[UsedImplicitly]
	public class CommandService : ICommandService
	{
		private readonly Subject<BottedMessage> _messageSubject = new();
		private readonly Subject<ICommandExecutionContext> _executionSubject = new();
		private readonly Dictionary<string, ICommandDataStructure> _dataStructures = new();
		private readonly IEventService _eventService;
		private readonly ICommandParser _parser;

		public CommandService(IEventService eventService, 
							  ICommandParser parser)
		{
			_eventService = eventService;
			_parser = parser;

			eventService.GetEvent<MessageReceived>().Subscribe(OnMessageReceived);

			_messageSubject.Subscribe(message =>
						   {
							   var context = ParseContext(message);
							   if (context is null)
							   {
								   return;
							   }
							   
							   _eventService.GetEvent<CommandExecuting>()
											.Raise(context);
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
			_executionSubject.SubscribeAsync(async context =>
			{
				if (context.CommandName != command.Name || !context.CanExecute)
				{
					return;
				}

				var result = await command.Execute((context.CommandData as TData)!);
				var message = new BottedMessage()
				{
					ChatId = context.Message.ChatId,
					ProviderIdentifier = context.Message.ProviderIdentifier,
					Text = result.Text
				};
				_eventService.GetEvent<MessageSent>().Raise(message);
			});
		}
		
		private void OnMessageReceived(BottedMessage message)
		{
			_messageSubject.OnNext(message);
		}

		private ICommandExecutionContext? ParseContext(BottedMessage message)
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
			return new CommandExecutionContext(commandName, data, message, true);
		}
	}
}