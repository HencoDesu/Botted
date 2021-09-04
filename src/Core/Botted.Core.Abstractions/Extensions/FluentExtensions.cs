using System;
using System.Collections.Generic;

namespace Botted.Core.Abstractions.Extensions
{
	public static class FluentExtensions
	{
		/// <summary>
		/// Applies some <see cref="Action{T}"/> to object and returns it
		/// </summary>
		/// <param name="object">Object to apply <see cref="Action{T}"/>></param>
		/// <param name="action">Action to apply to object</param>
		/// <typeparam name="T">Object type</typeparam>
		/// <returns>Object after applying</returns>
		public static T Apply<T>(this T @object, Action<T> action)
		{
			action(@object);
			return @object;
		}

		/// <summary>
		/// Applies some <see cref="Action{T}"/> to each object in <see cref="IEnumerable{T}"/>
		/// and returns that <see cref="IEnumerable{T}"/>
		/// </summary>
		/// <param name="enumerable">Enumerable to apply <see cref="Action{T}"/></param>
		/// <param name="action">Action to apply to each object</param>
		/// <typeparam name="T">Items type</typeparam>
		/// <returns>Enumerable after applying</returns>
		public static IEnumerable<T> Apply<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
				yield return item;
			}
		}
	}
}