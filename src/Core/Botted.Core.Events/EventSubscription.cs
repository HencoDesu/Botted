using System;
using Botted.Core.Events.Abstractions;

namespace Botted.Core.Events
{
	/// <inheritdoc />
	public class EventSubscription : IEventSubscription
	{
		public event Action? Unsubscribed;

		public void Dispose()
		{
			Unsubscribed?.Invoke();
			GC.SuppressFinalize(this);
		}
	}
}