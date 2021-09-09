using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions.Events;

namespace Botted.Core.Events.Abstractions.Extensions
{
	public static class ObservableExtensions
	{
		public static IDisposable Subscribe(this IObservable<Event> @event, 
											Action handler,
											Action<Exception>? errorHandler = null)
		{
			return errorHandler is null
				? @event.Select(_ => Unit.Default).Subscribe(_ => handler())
				: @event.Select(_ => Unit.Default).Subscribe(_ => handler(), errorHandler);
		}

		public static IDisposable Subscribe<T>(this IObservable<EventWithData<T>> @event, 
											   Action<T?> handler,
											   Action<Exception>? errorHandler = null)
		{
			return errorHandler is null
				? @event.Select(e => e.Data).Subscribe(handler)
				: @event.Select(e => e.Data).Subscribe(handler, errorHandler);
		}

		public static IDisposable SafeSubscribe<T>(this IObservable<EventWithData<T>> @event,
												   Action<T> handler,
												   Action<Exception>? errorHandler = null)
		{
			return errorHandler is null
				? @event.Select(e => e.Data).WhereNotNull().Subscribe(handler)
				: @event.Select(e => e.Data).WhereNotNull().Subscribe(handler, errorHandler);
		}
		
		public static IDisposable SubscribeAsync(this IObservable<Event> @event, 
												 Func<Task> handler, 
												 Action<Exception>? errorHandler = null)
		{
			return @event.Select(_ => Unit.Default).SubscribeAsync(handler, errorHandler);
		}

		public static IDisposable SubscribeAsync<T>(this IObservable<EventWithData<T>> @event, 
													Func<T?, Task> handler, 
													Action<Exception>? errorHandler = null)
		{
			return @event.Select(e => e.Data).SubscribeAsync(handler, errorHandler);
		}
		
		public static IDisposable SafeSubscribeAsync<T>(this IObservable<EventWithData<T>> @event, 
														Func<T, Task> handler, 
														Action<Exception>? errorHandler = null)
		{
			return @event.Select(e => e.Data).WhereNotNull().SubscribeAsync(handler, errorHandler);
		}

		public static IDisposable SubscribeAsync<T>(this IObservable<T> source,
													Func<Task> asyncAction, 
													Action<Exception>? handler = null)
		{
			return handler is null 
				? source.SelectMany(Wrapped).Subscribe(_ => { }) 
				: source.SelectMany(Wrapped).Subscribe(_ => { }, handler);
			
			async Task<Unit> Wrapped(T t)
			{
				await asyncAction();
				return Unit.Default;
			}
		}

		public static IDisposable SubscribeAsync<T>(this IObservable<T> source,
													Func<T, Task> asyncAction, 
													Action<Exception>? handler = null)
		{
			return handler is null 
				? source.SelectMany(Wrapped).Subscribe(_ => { }) 
				: source.SelectMany(Wrapped).Subscribe(_ => { }, handler);
			
			async Task<Unit> Wrapped(T t)
			{
				await asyncAction(t);
				return Unit.Default;
			}
		}

		public static IObservable<T> WhereNotNull<T>(this IObservable<T?> observable)
		{
			return observable.Where(o => o is not null)!;
		}
	}
}