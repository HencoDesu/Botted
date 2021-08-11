using System;
using Booted.Core.Events.Abstractions;

namespace Booted.Core.Events
{
	public class EventSubscription : IEventSubscription
	{
		public event Action? Unsubscribed;

		public void Unsubscribe()
		{
			Unsubscribed?.Invoke();
		}
	}
}