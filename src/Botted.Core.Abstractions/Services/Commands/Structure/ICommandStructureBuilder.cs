using System;
using System.Linq.Expressions;
using Botted.Core.Abstractions.Services.Commands.Data;

namespace Botted.Core.Abstractions.Services.Commands.Structure
{
	public interface ICommandStructureBuilder : IBuilder<ICommandStructure>
	{
		
	}

	public interface ICommandStructureBuilder<TData> : ICommandStructureBuilder
		where TData : ICommandData
	{
		ICommandStructureBuilder<TData> WithArgument<TArgument>(
			Expression<Func<TData, TArgument>> argument);
		
		ICommandStructureBuilder<TData> WithArgument<TArgument, TSource>(
			Expression<Func<TData, TArgument>> argument,
			Expression<Func<CommandParsingContext, TSource>> source);
		
		ICommandStructureBuilder<TData> WithEnumArgument<TArgument, TSource>(
			Expression<Func<TData, TArgument>> argument,
			Expression<Func<CommandParsingContext, TSource>> source)
			where TArgument : struct, Enum;
	}
}