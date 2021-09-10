using System.Collections.Generic;
using System.Linq;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using JetBrains.Annotations;

namespace Botted.Core.Events
{
	/// <inheritdoc />
	[UsedImplicitly]
	public class EventService : IEventService
	{
		private readonly List<Event> _events = new();
		
		public TEvent GetEvent<TEvent>() 
			where TEvent : Event, new()
		{
			var @event = _events.OfType<TEvent>().SingleOrDefault();
			if (@event is null)
			{
				@event = new TEvent();
				_events.Add(@event);
			}

			return @event;
		}
	}
}