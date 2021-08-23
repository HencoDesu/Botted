using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Events.Abstractions;

namespace Botted.Core.Events
{
	/// <inheritdoc />
	public class EventService : IEventService
	{
		private readonly Dictionary<IEvent, List<MulticastDelegate>> _handlers = new();

		public void Raise<TEvent>() 
			where TEvent : IEvent
		{
			var @event = GetEvent<TEvent>();
			if (@event is null)
			{
				return;
			}
			
			foreach (var handler in _handlers[@event].OfType<Action>())
			{
				handler();
			}
		}

		public void Raise<TEvent, TData>(TData data) 
			where TEvent : IEventWithData<TData>
		{
			var @event = GetEvent<TEvent>();
			if (@event is null)
			{
				return;
			}
			
			foreach (var handler in _handlers[@event].OfType<Action<TData>>())
			{
				handler(data);
			}
		}

		public IEventSubscription Subscribe<TEvent>(Action handler) 
			where TEvent : IEvent, new() 
			=> SubscribeTo<TEvent>(handler);

		public IEventSubscription Subscribe<TEvent, TData>(Action<TData> handler) 
			where TEvent : IEventWithData<TData>, new()
			=> SubscribeTo<TEvent>(handler);

		private IEvent? GetEvent<TEvent>() 
			where TEvent : IEvent 
			=> _handlers.Keys.OfType<TEvent>().SingleOrDefault();

		private IEventSubscription SubscribeTo<TEvent>(MulticastDelegate handler) 
			where TEvent : IEvent, new()
		{
			var subscription = new EventSubscription();
			var @event = GetEvent<TEvent>();
			if (@event is null)
			{
				@event = new TEvent();
				_handlers.Add(@event, new List<MulticastDelegate>());
			}
			
			subscription.Unsubscribed += () => Unsubscribe(@event, handler);
			_handlers[@event].Add(handler);
			
			return subscription;
		}

		private void Unsubscribe(IEvent @event, MulticastDelegate handler) 
			=> _handlers[@event].Remove(handler);
	}
}