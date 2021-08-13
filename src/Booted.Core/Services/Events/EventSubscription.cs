using System;
using Botted.Core.Abstractions.Services.Events;

namespace Booted.Core.Services.Events
{
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