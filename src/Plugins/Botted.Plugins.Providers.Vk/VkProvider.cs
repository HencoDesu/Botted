using System;
using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Messaging.Data;
using Botted.Core.Messaging.Services;
using Botted.Core.Users.Abstractions.Data;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Botted.Plugins.Providers.Vk
{
	public class VkProvider : AbstractMessageProvider
	{
		public static ProviderIdentifier Identifier { get; } = ProviderIdentifier.Create();

		private readonly IVkApi _vkApi;
		private readonly VkConfiguration _configuration;
		private readonly Random _random = new ();
		private readonly CancellationTokenSource _cts = new();
		
		public VkProvider(IEventService eventService,
						  IVkApi vkApi,
						  VkConfiguration configuration)
			: base(eventService, Identifier)
		{
			_vkApi = vkApi;
			_configuration = configuration;

			eventService.GetEvent<BotStarted>().Subscribe(OnBotStarted);
			eventService.GetEvent<BotStopped>().Subscribe(OnBotStopped);
		}

		public override async Task SendMessage(BottedMessage message)
		{
			await _vkApi.Messages.SendAsync(new MessagesSendParams()
			{
				PeerId = message.ChatId,
				Message = message.Text,
				RandomId = _random.Next()
			});
		}

		private void OnBotStarted()
		{
			var token = _cts.Token;
			Task.Run(() => WaitForUpdates(token), token);
		}

		private void OnBotStopped()
		{
			_cts.Cancel();
		}

		private void WaitForUpdates(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				var lpServer = _vkApi.Groups.GetLongPollServer(_configuration.GroupId);
				var actualTs = lpServer.Ts;

				try
				{
					while (!cancellationToken.IsCancellationRequested)
					{
						var lpResponse = _vkApi.Groups.GetBotsLongPollHistory(new BotsLongPollHistoryParams()
						{
							Key = lpServer.Key,
							Server = lpServer.Server,
							Ts = actualTs
						});
						actualTs = lpResponse.Ts;

						foreach (var groupUpdate in lpResponse.Updates)
						{
							if (groupUpdate.Type == GroupUpdateType.MessageNew)
							{
								var message = groupUpdate.Message;
								OnMessageReceived(ReadMessage(message));
							}
						}
					}
				} catch
				{
					// ignored for now
				}
			}
		}
		
		private BottedMessage ReadMessage(Message message)
		{
			return new BottedMessage()
			{
				ChatId = message.ChatId ?? message.PeerId,
				ProviderIdentifier = Identifier,
				Text = message.Text,
				Sender = new BottedUser()
			};
		}
	}
}