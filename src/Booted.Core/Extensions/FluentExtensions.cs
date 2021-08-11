using System;
using System.Collections.Generic;

namespace Booted.Core.Extensions
{
	public static class FluentExtensions
	{
		public static T WithField<T, TField>(this T @object,
											 ref TField field,
											 TField value)
		{
			field = value;
			return @object;
		}

		public static T WithListed<T, TItem>(this T @object,
											 IList<TItem> list,
											 TItem item)
		{
			list.Add(item);
			return @object;
		}

		public static T ChainInvoke<T>(this T @object,
									   Action<T> action)
		{
			action(@object);
			return @object;
		}

		public static T? SafeCast<T>(this object? @object)
			=> @object is null ? default : (T) @object;
	}
}