using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions.Delegates;

namespace Botted.Core.Events.Abstractions.Events
{
	public abstract class Event
	{
		private readonly List<BotEventHandler> _syncHandlers = new();
		private readonly List<AsyncEventHandler> _asyncEventHandlers = new();

		public void Subscribe(BotEventHandler handler)
			=> _syncHandlers.Add(handler);

		public void Subscribe(AsyncEventHandler handler)
			=> _asyncEventHandlers.Add(handler);

		public void Unsubscribe(BotEventHandler handler)
			=> _syncHandlers.Remove(handler);

		public void Unsubscribe(AsyncEventHandler handler)
			=> _asyncEventHandlers.Remove(handler);

		public virtual Task Handle(object? data) 
			=> Handle();
		
		private Task Handle()
		{
			foreach (var syncHandler in _syncHandlers)
			{
				syncHandler();
			}

			return Task.WhenAll(_asyncEventHandlers.Select(h => h.Invoke()));
		}
	}
}