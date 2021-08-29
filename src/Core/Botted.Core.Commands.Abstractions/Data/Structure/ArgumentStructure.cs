using System;
using System.Reflection;
using Botted.Core.Commands.Abstractions.Context;

namespace Botted.Core.Commands.Abstractions.Data.Structure
{
	/// <inheritdoc />
	public class ArgumentStructure : IArgumentStructure
	{
		public ArgumentStructure(MethodInfo targetProperty, 
								 Func<ICommandParsingContext, object?> dataSource,
								 bool optional)
		{
			TargetProperty = targetProperty;
			DataSource = dataSource;
			Optional = optional;
		}

		public MethodInfo TargetProperty { get; }

		public Func<ICommandParsingContext, object?> DataSource { get; }

		public bool Optional { get; }
	}
}