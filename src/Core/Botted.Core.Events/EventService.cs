using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Delegates;
using Botted.Core.Events.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Botted.Core.Events
{
	/// <inheritdoc />
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "It's a service that will be initialized via DI container")]
	public class EventService : IEventService
	{
		private readonly ILogger<EventService> _logger;
		private readonly List<Event> _events = new();
		private readonly ConcurrentQueue<(Event, object?)> _eventQueue = new ();
		private readonly Thread _queueHandlerThread;

		public EventService(ILogger<EventService> logger)
		{
			_logger = logger;
			_queueHandlerThread = new Thread(ProcessQueue)
			{
				IsBackground = true,
				Name = "EventService Queue Handler",
				Priority = ThreadPriority.Normal
			};

			Subscribe<BotStarted>(OnBotStarted);
		}

		public void Raise<TEvent>() 
			where TEvent : Event
		{
			TryEnqueueEvent<TEvent>(null);
		}

		public async Task RaiseAndWait<TEvent>() 
			where TEvent : Event
		{
			var @event = GetEvent<TEvent>();
			if (@event is not null)
			{
				await @event.Handle(null);
			}
		}

		public void Raise<TEvent, TData>(TData data) 
			where TEvent : EventWithData<TData>
		{
			TryEnqueueEvent<TEvent>(data);
		}

		public async Task RaiseAndWait<TEvent, TData>(TData data) 
			where TEvent : EventWithData<TData>
		{
			await HandleEvent<TEvent>(data).ConfigureAwait(false);
		}

		public IEventSubscription Subscribe<TEvent>(BotEventHandler handler) 
			where TEvent : Event, new()
		{
			var subscription = new EventSubscription();
			var @event = GetOrRegisterEvent<TEvent>();
			@event.Subscribe(handler);
			subscription.Unsubscribed += () => @event.Unsubscribe(handler);
			return subscription;
		}
		
		public IEventSubscription Subscribe<TEvent>(AsyncEventHandler handler)
			where TEvent : Event, new()
		{
			var subscription = new EventSubscription();
			var @event = GetOrRegisterEvent<TEvent>();
			@event.Subscribe(handler);
			subscription.Unsubscribed += () => @event.Unsubscribe(handler);
			return subscription;
		}

		public IEventSubscription Subscribe<TEvent, TData>(BotEventHandler<TData> handler) 
			where TEvent : EventWithData<TData>, new()
		{
			var subscription = new EventSubscription();
			var @event = GetOrRegisterEvent<TEvent>();
			@event.Subscribe(handler);
			subscription.Unsubscribed += () => @event.Unsubscribe(handler);
			return subscription;
		}

		public IEventSubscription Subscribe<TEvent, TData>(AsyncEventHandler<TData> handler) 
			where TEvent : EventWithData<TData>, new()
		{
			var subscription = new EventSubscription();
			var @event = GetOrRegisterEvent<TEvent>();
			@event.Subscribe(handler);
			subscription.Unsubscribed += () => @event.Unsubscribe(handler);
			return subscription;
		}

		private TEvent? GetEvent<TEvent>()
			where TEvent : Event
			=> _events.OfType<TEvent>().SingleOrDefault();

		private TEvent RegisterEvent<TEvent>()
			where TEvent : Event, new()
		{
			var @event = new TEvent();
			_events.Add(@event);
			return @event;
		}

		private TEvent GetOrRegisterEvent<TEvent>()
			where TEvent : Event, new() 
			=> GetEvent<TEvent>() ?? RegisterEvent<TEvent>();

		private void TryEnqueueEvent<TEvent>(object? data)
			where TEvent : Event
		{
			var @event = GetEvent<TEvent>();
			if (@event is not null)
			{
				_eventQueue.Enqueue((@event, data));
			}
		}

		private async Task HandleEvent<TEvent>(object? data)
			where TEvent : Event
		{
			var @event = GetEvent<TEvent>();
			if (@event is not null)
			{
				await @event.Handle(data).ConfigureAwait(false);
			}
		}

		private void OnBotStarted()
		{
			_queueHandlerThread.Start();
		}
		
		[SuppressMessage("ReSharper", "FunctionNeverReturns")]
		private async void ProcessQueue()
		{
			while (true)
			{
				if (_eventQueue.TryDequeue(out var queuedItem))
				{
					var (@event, data) = queuedItem;
					try
					{
						await @event.Handle(data).ConfigureAwait(false);
					} catch (System.Exception exception)
					{
						_logger.LogError(exception, "Error during event {0} handling", @event.GetType().Name);
					}
				}
			}
		}
	}
}