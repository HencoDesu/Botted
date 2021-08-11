using System;
using System.Linq.Expressions;
using Booted.Core.Commands.Abstractions;

namespace Booted.Core.Extensions
{
	public static class CommandBuilderExtensions
	{
		public static ICommandBuilder<TData> WithArgument<TData>(this ICommandBuilder<TData> builder,
																 Expression<Func<TData, string>> expression)
			where TData : new() 
			=> builder.WithArgument(expression, s => s);

		public static ICommandBuilder<TData> WithArgument<TData>(this ICommandBuilder<TData> builder,
																 Expression<Func<TData, int>> expression)
			where TData : new() 
			=> builder.WithArgument(expression, int.Parse);
	}
}