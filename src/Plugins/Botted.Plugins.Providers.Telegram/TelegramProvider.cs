using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Messaging.Data;
using Botted.Core.Messaging.Services;
using Botted.Core.Users.Abstractions.Data;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Botted.Plugins.Providers.Telegram
{
	public class TelegramProvider : AbstractMessageProvider
	{
		public static ProviderIdentifier Identifier { get; } = ProviderIdentifier.Create();

		private readonly ITelegramBotClient _telegramBotClient;
		private readonly CancellationTokenSource _cts = new();

		public TelegramProvider(IEventService eventService,
								ITelegramBotClient telegramBotClient)
			: base(eventService, Identifier)
		{
			_telegramBotClient = telegramBotClient;

			eventService.GetEvent<BotStarted>().Subscribe(OnBotStarted);
			eventService.GetEvent<BotStopped>().Subscribe(OnBotStopped);
		}

		public override async Task SendMessage(BottedMessage message)
		{
			await _telegramBotClient.SendTextMessageAsync(message.ChatId, message.Text);
		}

		private void OnBotStarted()
		{
			_telegramBotClient.StartReceiving(HandleUpdateAsync, (_, _, _) => Task.CompletedTask, null, _cts.Token);
		}

		private void OnBotStopped()
		{
			_cts.Cancel();
		}

		private Task HandleUpdateAsync(ITelegramBotClient botClient,
									   Update update,
									   CancellationToken cancellationToken)
		{
			if (update.Type == UpdateType.Message && 
				update.Message is not null && 
				update.Message.Type == MessageType.Text && 
				update.Message.Text is not null)
			{
				OnMessageReceived(ReadMessage(update.Message));
			}

			return Task.CompletedTask;
		}

		private BottedMessage ReadMessage(Message message)
		{
			return new BottedMessage()
			{
				Text = message.Text,
				ProviderIdentifier = Identifier,
				ChatId = message.Chat.Id,
				Sender = new BottedUser()
			};
		}
	}
}