using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Abstractions;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using Microsoft.Extensions.Hosting;

namespace Botted.Core
{
	/// <inheritdoc />
	public class Bot : IBot
	{
		private readonly IEventService _eventService;

		public Bot(IEventService eventService, IHostApplicationLifetime applicationLifetime)
		{
			_eventService = eventService;
		}

		/// <inheritdoc />
		public Task StartAsync(CancellationToken cancellationToken)
		{
			_eventService.GetEvent<BotStarted>()
						 .Raise();
			
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}