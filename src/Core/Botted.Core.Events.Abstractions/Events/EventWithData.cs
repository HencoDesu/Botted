using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions.Extensions;

namespace Botted.Core.Events.Abstractions.Events
{
	/// <summary>
	/// Represents some event with some additional data
	/// that can be raised or subscribed.
	/// Also can be called and handled without any data
	/// </summary>
	/// <remarks>
	/// You don't need to create it your self's, just get it though
	/// <see cref="IEventService.GetEvent{TEvent}"/>
	/// </remarks>
	public abstract class EventWithData<TData> : Event
	{
		private readonly Subject<TData> _subject = new();

		/// <inheritdoc cref="Event.Raise"/>
		/// <param name="data">Event data</param>
		public void Raise(TData data)
		{
			base.Raise();
			_subject.OnNext(data);
		}

		/// <inheritdoc cref="Event.Subscribe(Action)"/>
		public IDisposable Subscribe(Action<TData> handler)
		{
			return _subject.Subscribe(handler);
		}

		/// <inheritdoc cref="Event.Subscribe(Action)"/>
		public IDisposable Subscribe(Func<TData, Task> handler)
		{
			return _subject.SubscribeAsync(handler);
		}
	}
}