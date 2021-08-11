namespace Booted.Core.Events.Abstractions
{
	public interface IEvent { }

	public interface IEvent<TData> : IEvent { }
}