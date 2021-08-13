using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Botted.Core.Abstractions.Services.Commands.Structure;

namespace Botted.Core.Abstractions.Extensions
{
	[SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
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