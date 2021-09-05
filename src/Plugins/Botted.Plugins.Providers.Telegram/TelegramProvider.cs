using System;
using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Providers.Abstractions;
using Botted.Core.Providers.Abstractions.Data;
using Serilog;
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
			eventService.Subscribe<BotStarted>(OnBotStarted);
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
			var ErrorMessage = exception switch
			{
				ApiRequestException apiRequestException =>
					$"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
				_ => exception.ToString()
			};

			Console.WriteLine(ErrorMessage);
			return Task.CompletedTask;
		}

		private async Task HandleUpdateAsync(ITelegramBotClient botClient,
											 Update update,
											 CancellationToken cancellationToken)
		{
			if (update.Type != UpdateType.Message)
				return;
			if (update.Message.Type != MessageType.Text)
				return;

			Log.Logger.Information("Received a '{0}' message in chat {1}.", update.Message.Text,
								   update.Message.Chat.Id);
			var message = new Message(update.Message.Text, Identifier, null);
			message.SetAdditionalData(update.Message);
			OnMessageReceived(message);
		}
	}
}