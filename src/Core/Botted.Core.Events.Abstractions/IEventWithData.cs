using System.Diagnostics.CodeAnalysis;

namespace Botted.Core.Events.Abstractions
{
	/// <summary>
	/// Represents an event that can occurs in bot. <br/>
	/// Used in <see cref="IEventService"/>
	/// </summary>
	/// <typeparam name="TData">Event data</typeparam>
	[SuppressMessage("ReSharper", "UnusedTypeParameter")]
	public interface IEventWithData<TData> : IEvent { }
}