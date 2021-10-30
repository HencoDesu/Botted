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
				? source.Subscribe(_ => asyncAction().Start()) 
				: source.Subscribe(_ => asyncAction().Start(), handler);
		}

		public static IDisposable SubscribeAsync<T>(this IObservable<T> source,
													Func<T, Task> asyncAction, 
													Action<Exception>? handler = null)
		{
			return handler is null 
				? source.Subscribe(_ => asyncAction(_).Start()) 
				: source.Subscribe(_ => asyncAction(_).Start(), handler);
		}
	}
}