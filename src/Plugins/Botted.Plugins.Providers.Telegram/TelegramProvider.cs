using System;
using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Providers.Abstractions;
using Botted.Core.Providers.Abstractions.Data;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramMessage = Telegram.Bot.Types.Message;
using Message = Botted.Core.Providers.Abstractions.Data.Message;

namespace Botted.Plugins.Providers.Telegram
{
	public class TelegramProvider : AbstractProviderService
	{
		public static ProviderIdentifier Identifier { get; } = new();

		private readonly ITelegramBotClient _telegramBotClient;

		public TelegramProvider(IEventService eventService,
								ITelegramBotClient telegramBotClient)
			: base(eventService, Identifier)
		{
			_telegramBotClient = telegramBotClient;

			eventService.GetEvent<BotStarted>().Subscribe(OnBotStarted);
		}

		public override async Task SendMessage(Message message)
		{
			var tgMessage = message.GetAdditionalData<TelegramMessage>();
			await _telegramBotClient.SendTextMessageAsync(tgMessage.Chat.Id, message.Text);
		}

		private void OnBotStarted()
		{
			_telegramBotClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync);
		}

		private Task HandleErrorAsync(ITelegramBotClient botClient,
									  Exception exception,
									  CancellationToken cancellationToken)
		{
			var errorMessage = exception switch
			{
				ApiRequestException apiRequestException =>
					$"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
				_ => exception.ToString()
			};

			Console.WriteLine(errorMessage);
			return Task.CompletedTask;
		}

		private Task HandleUpdateAsync(ITelegramBotClient botClient,
											 Update update,
											 CancellationToken cancellationToken)
		{
			if (update.Type != UpdateType.Message && update.Message?.Type != MessageType.Text)
			{
				return Task.CompletedTask;
			}

			var message = new Message(update.Message.Text, Identifier, null);
			message.SetAdditionalData(update.Message);
			OnMessageReceived(message);
		}
	}
}