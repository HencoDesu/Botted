using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Commands.Structure.Abstractions;

namespace Booted.Core.Commands.Structure
{
	public class CommandStructureBuilder<TData> : ICommandStructureBuilder<TData> 
		where TData : class, ICommandData, new()
	{
		private readonly List<IArgumentStructure> _arguments = new ();
		public ICommandStructureBuilder<TData> WithArgument<TArgument>(Expression<Func<TData, TArgument>> expression, 
																	   Func<string, TArgument> converter)
		{
			var member = expression.Body as MemberExpression;
			var argument = new ArgumentStructure<TArgument>(member.Member as PropertyInfo, converter);
			_arguments.Add(argument);
			return this;
		}

		public ICommandStructure Build()
		{
			return new CommandStructure<TData>(_arguments);
		}
	}
}