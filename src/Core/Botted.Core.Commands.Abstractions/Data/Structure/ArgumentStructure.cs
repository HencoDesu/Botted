using System.Reflection;

namespace Botted.Core.Commands.Abstractions.Data.Structure
{
	/// <inheritdoc />
	public class ArgumentStructure : IArgumentStructure
	{
		public ArgumentStructure(MethodInfo targetProperty, 
								 MethodInfo dataSource,
								 bool optional)
		{
			TargetProperty = targetProperty;
			DataSource = dataSource;
			Optional = optional;
		}

		public MethodInfo TargetProperty { get; }

		public MethodInfo DataSource { get; }

		public bool Optional { get; }
	}
}