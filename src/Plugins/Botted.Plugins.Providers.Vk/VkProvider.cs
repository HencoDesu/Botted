using System.Threading;
using System.Threading.Tasks;
using Botted.Core.AdditionalData.Abstractions;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Providers.Abstractions;
using Botted.Core.Providers.Abstractions.Data;
using Botted.Core.Providers.Abstractions.Events;
using Botted.Core.Users.Abstractions.Data;
using Microsoft.Extensions.Logging;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace Botted.Plugins.Providers.Vk
{
	public class VkProvider : AbstractProviderService
	{
		public static ProviderIdentifier Identifier { get; } = new();

		private readonly IVkApi _vkApi;
		private readonly VkConfiguration _configuration;
		private readonly ILogger<VkProvider> _logger;
		private readonly CancellationTokenSource _cts = new();
		
		public VkProvider(IEventService eventService,
						  IVkApi vkApi,
						  VkConfiguration configuration,
						  ILogger<VkProvider> logger)
			: base(eventService, Identifier)
		{
			_vkApi = vkApi;
			_configuration = configuration;
			_logger = logger;

			eventService.GetEvent<BotStarted>().Subscribe(OnBotStarted);
			eventService.GetEvent<BotStopped>().Subscribe(OnBotStopped);
		}

		public override async Task SendMessage(Message message)
		{
			var vkMessage = message.GetAdditionalData<VkNet.Model.Message>();
			await _vkApi.Messages.SendAsync(new MessagesSendParams()
			{
				PeerId = vkMessage.PeerId,
				Message = message.Text
			});
		}

		protected override void WaitForUpdates(CancellationToken cancellationToken)
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
								var bottedMessage = new Message(message.Text, Identifier, new User())
									.WithAdditionalData(message);
								_logger.LogInformation(message.Text);
								OnMessageReceived(bottedMessage);
							}
						}
					}
				} catch
				{
					// ignored for now
				}
			}
		}
	}
}