namespace Botted.Core.Abstractions.Services.Events
{
	public interface IEvent
	{ }

	// ReSharper disable once UnusedTypeParameter
	public interface IEvent<TData> : IEvent
	{ }
}