using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Events;
using JetBrains.Annotations;

namespace Botted.Core.Events
{
	/// <inheritdoc />
	[UsedImplicitly]
	public class EventService : IEventService
	{
		private readonly Subject<object> _subject = new ();

		public IObservable<TEvent> GetEvent<TEvent>() 
			where TEvent : Event
		{
			return _subject.OfType<TEvent>();
		}
		
		public void Raise<TEvent>() 
			where TEvent : Event, new()
		{
			_subject.OnNext(new TEvent());
		}

		public void Raise<TEvent, TData>(TData data) 
			where TEvent : EventWithData<TData>, new()
		{
			_subject.OnNext(new TEvent { Data = data });
		}
	}
}