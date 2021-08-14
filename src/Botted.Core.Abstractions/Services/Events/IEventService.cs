using System;
using System.Diagnostics.CodeAnalysis;

namespace Botted.Core.Abstractions.Services.Events
{
	[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	[SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
	public interface IEventService : IService
	{
		void Rise<TEvent>()
			where TEvent : IEvent;

		void Rise<TEvent, TData>(TData data) 
			where TEvent : IEvent<TData>;
		
		IEventSubscription Subscribe<TEvent>(Action handler) 
			where TEvent : IEvent;
		
		IEventSubscription Subscribe<TEvent, TData>(Action<TData> handler) 
			where TEvent : IEvent<TData>;
	}
}