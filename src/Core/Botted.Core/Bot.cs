using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Abstractions;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;

namespace Botted.Core
{
	/// <inheritdoc />
	public class Bot : IBot
	{
		private readonly IEventBottedService _eventBottedService;

		public Bot(IEventBottedService eventBottedService)
		{
			_eventBottedService = eventBottedService;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_eventBottedService.GetEvent<BotStarted>()
						 .Raise();
			
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_eventBottedService.GetEvent<BotStopped>()
						 .Raise();
			
			return Task.CompletedTask;
		}
	}
}