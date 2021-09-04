using System;

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
	}
}