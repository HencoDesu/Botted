using System;
using System.Linq.Expressions;
using Booted.Core.Commands.Structure.Abstractions;

namespace Booted.Core.Extensions
{
	public static class CommandStructureBuilderExtensions
	{
		public static ICommandStructureBuilder<TData> WithArgument<TData>(
			this ICommandStructureBuilder<TData> builder,
			Expression<Func<TData, string>> expression)
			=> builder.WithArgument(expression, s => s);

		public static ICommandStructureBuilder<TData> WithArgument<TData>(
			this ICommandStructureBuilder<TData> builder,
			Expression<Func<TData, int>> expression)
			=> builder.WithArgument(expression, int.Parse);
	}
}