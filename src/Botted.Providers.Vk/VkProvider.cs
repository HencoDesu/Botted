using System;
using System.Linq;
using System.Threading;
using Booted.Core.Providers;
using VkNet.Abstractions;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Botted.Providers.Vk
{
	public class VkProvider : IProvider
	{
		public static ProviderIdentifier Identifier { get; } = new();
		
		private readonly IVkApi _vkApi;
		private readonly Thread _longPollThread;
		private readonly Random _rnd;
		private bool _poolingEnabled;

		public VkProvider(IVkApi vkApi)
		{
			_vkApi = vkApi;
			_longPollThread = new Thread(DoPoll);
			_rnd = new Random();
		}

		public event Action<IProvider, BotMessage> MessageReceived;
		
		public void SendMessage(BotMessage message)
		{
			_vkApi.Messages.Send(new MessagesSendParams()
			{
				PeerId = message.UserId, 
				Message = message.Text,
				RandomId = _rnd.Next()
			});
		}

		public void StartPolling()
		{
			_poolingEnabled = true;
			_longPollThread.Start();
		}

		public void StopPolling()
		{
			_poolingEnabled = false;
		}

		private void DoPoll()
		{
			while (_poolingEnabled)
			{
				var lpServer = _vkApi.Messages.GetLongPollServer(true);
				var ts = Convert.ToUInt64(lpServer.Ts);
				try
				{
					var pts = lpServer.Pts;
					while (_poolingEnabled)
					{
						var lpResponse = _vkApi.Messages.GetLongPollHistory(new MessagesGetLongPollHistoryParams
						{
							Ts = ts,
							Pts = pts,
						});
						pts = lpResponse.NewPts;

						foreach (var message in lpResponse.Messages.Where(m => !m.Out ?? true))
						{
							MessageReceived?.Invoke(this, PrepareMessage(message));
						}
					}
				} 
				catch (LongPollException e)
				{
					//ignored
				}
			}
		}

		private static BotMessage PrepareMessage(Message message)
		{
			return new BotMessage()
			{
				Provider = Identifier,
				Text = message.Text,
				UserId = message.FromId ?? 0,
			};
		}
	}
}