using System;

namespace Botted.Core.Events.Abstractions
{
	/// <summary>
	/// Represents a event subscription that allows to unsubscribe
	/// by call <see cref="IDisposable.Dispose"/>
	/// </summary>
	public interface IEventSubscription : IDisposable { }
}