using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Botted.Core.Commands.Abstractions.Data.Structure
{
	/// <inheritdoc />
	public class CommandDataStructureBuilder<TData> : ICommandDataStructureBuilder<TData> 
		where TData : class, ICommandData
	{
		private readonly Func<TData> _factory;
		private readonly List<IArgumentStructure> _arguments = new();

		public CommandDataStructureBuilder(Func<TData> factory)
		{
			_factory = factory;
		}

		public ICommandDataStructureBuilder<TData> WithArgument<TArgument, TSource>(
			Expression<Func<TData, TArgument>> argument, 
			Expression<Func<ICommandParsingContext, TSource>> source,
			bool optional = false)
		{
			var targetMethod = argument.Body switch
			{
				MemberExpression { Member: PropertyInfo { SetMethod: { } } property } => property.SetMethod,
				_ => throw new Exception() // TODO: Custom exception here
			};

			var sourceMethod = source.Compile();
			
			var argumentStructure = new ArgumentStructure(targetMethod, c => sourceMethod(c), optional);
			_arguments.Add(argumentStructure);
			return this;
		}

		public ICommandDataStructure Build()
		{
			return new CommandDataStructure(_arguments, () => _factory());
		}
	}
}