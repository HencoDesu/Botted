using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Structure;

namespace Botted.Core.Services.Commands.Structure
{
	public class CommandStructureBuilder<TData> : ICommandStructureBuilder<TData> 
		where TData : class, ICommandData, new()
	{
		private readonly List<IArgumentStructure> _arguments = new ();
		public ICommandStructureBuilder<TData> WithArgument<TArgument>(Expression<Func<TData, TArgument>> expression, 
																	   Func<string, TArgument> converter)
		{
			try
			{
				var member = expression.Body as MemberExpression;
				var property = member!.Member as PropertyInfo;
				var argument = new ArgumentStructure<TArgument>(property!, converter);
				_arguments.Add(argument);
				return this;
			} catch (NullReferenceException _)
			{
				// TODO: Custom exception type here
				throw new Exception("Only properties can be an argument", _);
			}
		}

		public ICommandStructure Build()
		{
			return new CommandStructure<TData>(_arguments);
		}
	}
}