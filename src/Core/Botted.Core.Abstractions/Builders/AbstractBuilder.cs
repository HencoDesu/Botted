using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Botted.Core.Abstractions.Builders
{
	/// <summary>
	/// Provides some helpful methods to made builders code a bit easier
	/// </summary>
	/// <typeparam name="TBuilder">Builder type</typeparam>
	/// <typeparam name="TResult">Builder result type</typeparam>
	public abstract class AbstractBuilder<TBuilder, TResult>
		: IBuilder<TResult>
		where TBuilder : AbstractBuilder<TBuilder, TResult>
	{
		public abstract TResult Build();

		/// <summary>
		/// Sets field value
		/// </summary>
		/// <param name="field">Reference to field</param>
		/// <param name="value">Value to set</param>
		/// <typeparam name="T">Value type</typeparam>
		/// <returns>This builder</returns>
		[SuppressMessage("ReSharper", "RedundantAssignment")]
		protected TBuilder SetField<T>(ref T field, T value)
		{
			field = value;
			return (TBuilder) this;
		}

		/// <summary>
		/// Adds value to some <see cref="IList{T}"/>
		/// </summary>
		/// <param name="list">List to add value</param>
		/// <param name="value">Value to add</param>
		/// <typeparam name="T">Value type</typeparam>
		/// <returns>This builder</returns>
		protected TBuilder AddToList<T>(IList<T> list, T value)
		{
			list.Add(value);
			return (TBuilder) this;
		}

		/// <summary>
		/// Invokes some action with provided argument
		/// </summary>
		/// <param name="action">Action to invoke</param>
		/// <param name="argument">Argument to pass in action</param>
		/// <typeparam name="T">Argument type</typeparam>
		/// <returns>This builder</returns>
		protected TBuilder InvokeActionWithArgument<T>(Action<T> action, T argument)
		{
			action(argument);
			return (TBuilder) this;
		}
	}
}