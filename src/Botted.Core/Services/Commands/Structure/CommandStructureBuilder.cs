using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Autofac;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Data;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Abstractions.Services.Parsing;

namespace Botted.Core.Services.Commands.Structure
{
	public class CommandStructureBuilder : ICommandStructureBuilder
	{
		/// <inheritdoc />
		public ICommandStructure Build()
			=> new CommandStructure();
	}

	public class CommandStructureBuilder<TData> : ICommandStructureBuilder<TData>
		where TData : ICommandData, new()
	{
		private readonly ILifetimeScope _container;
		private readonly List<IArgumentStructure> _arguments = new();

		public CommandStructureBuilder(ILifetimeScope container)
		{
			_container = container;
		}
		
		public ICommandStructureBuilder<TData> WithArgument<TArgument>(
			Expression<Func<TData, TArgument>> argument)
		{
			return WithArgument(argument, 
								c => c.TextArgs[0], 
								_container.Resolve<IConverter<string, TArgument>>().Convert);
		}

		public ICommandStructureBuilder<TData> WithArgument<TArgument, TSource>(
			Expression<Func<TData, TArgument>> argument,
			Expression<Func<CommandParsingContext, TSource>> source)
		{
			return WithArgument(argument, 
								source, 
								_container.Resolve<IConverter<TSource, TArgument>>().Convert);
		}
		
		public ICommandStructureBuilder<TData> WithEnumArgument<TArgument, TSource>(
			Expression<Func<TData, TArgument>> argument,
			Expression<Func<CommandParsingContext, TSource>> source)
			where TArgument : struct, Enum
		{
			return WithArgument(argument, 
								source, 
								_container.Resolve<IEnumConverter<TSource>>().Convert<TArgument>);
		}
		
		private ICommandStructureBuilder<TData> WithArgument<TArgument, TSource>(
			Expression<Func<TData, TArgument>> argument, 
			Expression<Func<CommandParsingContext, TSource>> source, 
			Func<TSource, TArgument> converter)
		{
			var targetMethod = argument.Body switch
			{
				MemberExpression { Member: PropertyInfo { SetMethod: { } } property } => property.SetMethod,
				_ => null
			};
			
			var sourceMethod = source.Body switch
			{
				MemberExpression { Member: PropertyInfo { GetMethod: { } } property } => property.GetMethod,
				MethodCallExpression method => method.Method,
				_ => null
			};
			
			var argumentBuilder = new ArgumentStructureBuilder<TData, TArgument, TSource>();
			argumentBuilder.WithSource(sourceMethod)
						   .WithTarget(targetMethod)
						   .WithConverter(converter);
			
			_arguments.Add(argumentBuilder.Build());
			return this;
		}

		public ICommandStructure Build()
		{
			return new CommandStructure<TData>(_arguments);
		}
	}
}