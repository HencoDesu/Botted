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

		public void Start()
		{
			_eventService.Raise<BotStarted>();
		}

		public void Stop()
		{
			
		}
	}
}