using System;
using Botted.Core.Abstractions.Services.Events;

namespace Botted.Core.Services.Events
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