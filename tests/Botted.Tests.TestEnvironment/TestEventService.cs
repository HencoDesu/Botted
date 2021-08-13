using System.Collections.Generic;
using System.Linq;
using Botted.Core.Services.Events;
using Botted.Core.Abstractions.Services.Events;

namespace Botted.Tests.TestEnvironment
{
	public class TestEventService : EventService
	{
		private readonly List<IEvent> _raisedEvents = new();
		private readonly IEvent[] _events;

		public TestEventService(params IEvent[] events) : base(events)
		{
			_events = events;
		}

		public bool IsRaised(IEvent @event)
			=> _raisedEvents.Contains(@event);

		public override void Raise<TEvent>()
		{
			MarkRaised<TEvent>();
			base.Raise<TEvent>();
		}

		public override void Raise<TEvent, TData>(TData data)
		{
			MarkRaised<TEvent>();
			base.Raise<TEvent, TData>(data);
		}

		private void MarkRaised<TEvent>()
			where TEvent : IEvent
		{
			_raisedEvents.Add(_events.OfType<TEvent>().First());
		}
	}
}