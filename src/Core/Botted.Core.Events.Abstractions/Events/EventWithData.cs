using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions.Delegates;

namespace Botted.Core.Events.Abstractions.Events
{
	public abstract class EventWithData<TData> : Event
	{
		private readonly List<BotEventHandler<TData>> _syncHandlers = new();
		private readonly List<AsyncEventHandler<TData>> _asyncEventHandlers = new();

		public void Subscribe(BotEventHandler<TData> handler)
			=> _syncHandlers.Add(handler);

		public void Subscribe(AsyncEventHandler<TData> handler)
			=> _asyncEventHandlers.Add(handler);

		public void Unsubscribe(BotEventHandler<TData> handler)
			=> _syncHandlers.Remove(handler);

		public void Unsubscribe(AsyncEventHandler<TData> handler)
			=> _asyncEventHandlers.Remove(handler);

		public override Task Handle(object? data)
		{
			return data is null or not TData
				? base.Handle(data)
				: Handle((TData) data);
		}

		private Task Handle(TData data)
		{
			foreach (var syncHandler in _syncHandlers)
			{
				syncHandler(data);
			}

			return Task.WhenAll(_asyncEventHandlers.Select(h => h.Invoke(data)));
		}
	}
}