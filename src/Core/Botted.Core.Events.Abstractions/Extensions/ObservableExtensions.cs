using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Botted.Core.Events.Abstractions.Events;

namespace Botted.Core.Events.Abstractions.Extensions
{
	public static class ObservableExtensions
	{
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