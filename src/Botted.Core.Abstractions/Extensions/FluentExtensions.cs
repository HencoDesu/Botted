using System;

namespace Botted.Core.Abstractions.Extensions
{
	public static class FluentExtensions
	{
		public static T Invoke<T>(
			this T target, 
			Action<T>? action)
		{
			action?.Invoke(target);
			return target;
		}

		public static TOut Invoke<TIn, TOut>(
			this TIn target,
			Func<TIn, TOut> func)
			=> func(target);
	}
}