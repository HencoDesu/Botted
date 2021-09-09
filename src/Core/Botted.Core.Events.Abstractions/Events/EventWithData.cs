namespace Botted.Core.Events.Abstractions.Events
{
	public abstract class EventWithData<TData> : Event
	{
		public TData? Data { get; init; }
	}
}