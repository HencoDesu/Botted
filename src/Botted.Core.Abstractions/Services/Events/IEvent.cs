namespace Botted.Core.Abstractions.Services.Events
{
	public interface IEvent
	{ }

	public interface IEvent<TData> : IEvent
	{ }
}