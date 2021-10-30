using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions.Extensions;

namespace Botted.Core.Events.Abstractions.Events
{
	/// <summary>
	/// Represents some event without any additional data
	/// that can be raised or subscribed
	/// </summary>
	/// <remarks>
	/// You don't need to create it your self's, just get it though
	/// <see cref="IEventBottedService.GetEvent{TEvent}"/>
	/// </remarks>
	public abstract class Event
	{
		private readonly object _emptyObject = new();
		private readonly Subject<object> _subject = new();

		/// <summary>
		/// Raises event.
		/// All handlers registered trough <see cref="Subscribe(Action)"/> or
		/// <see cref="Subscribe(Func{Task})"/> will be invoked
		/// </summary>
		public void Raise()
		{
			_subject.OnNext(_emptyObject);
		}

		/// <summary>
		/// Subscribes handler for event.
		/// Handler will be invoked when anyone calls <see cref="Raise"/>
		/// </summary>
		/// <param name="handler">Handler for this event</param>
		/// <returns>Event subscription</returns>
		public IDisposable Subscribe(Action handler)
		{
			return _subject.Subscribe(_ => handler());
		}

		/// <inheritdoc cref="Subscribe(Action)"/>
		public IDisposable Subscribe(Func<Task> handler)
		{
			return _subject.SubscribeAsync(handler);
		}
	}
}