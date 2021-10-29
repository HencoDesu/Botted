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
		private readonly IEventService _eventService;

		public Bot(IEventService eventService)
		{
			_eventService = eventService;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_eventService.GetEvent<BotStarted>()
						 .Raise();
			
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_eventService.GetEvent<BotStopped>()
						 .Raise();
			
			return Task.CompletedTask;
		}
	}
}