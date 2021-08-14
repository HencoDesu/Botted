using System.Collections.Generic;
using System.Linq;
using Botted.Core.Services.Events;
using Botted.Core.Abstractions.Services.Events;

namespace Botted.Tests.TestEnvironment
{
	public class TestEventService : EventService
	{
		private readonly List<IEvent> _raisedEvents = new();
		private readonly Dictionary<IEvent, object> _eventData = new();
		private readonly IEvent[] _events;

		public TestEventService(params IEvent[] events) : base(events)
		{
			_events = events;
		}

		public bool IsRaised(IEvent @event)
			=> _raisedEvents.Contains(@event);

		public TData GetLastData<TEvent, TData>()
			where TEvent : IEvent<TData>
		{
			var @event = _events.OfType<TEvent>().First();
			return (TData)_eventData[@event];
		}

		public override void Rise<TEvent>()
		{
			_raisedEvents.Add(_events.OfType<TEvent>().First());
			base.Rise<TEvent>();
		}

		public override void Rise<TEvent, TData>(TData data)
		{
			var @event = _events.OfType<TEvent>().First();
			_raisedEvents.Add(@event);
			_eventData[@event] = data;
			base.Rise<TEvent, TData>(data);
		}
	}
}