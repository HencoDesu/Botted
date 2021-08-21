using System;
using System.Reflection;
using Botted.Core.Abstractions.Services.Commands.Structure;

namespace Botted.Core.Services.Commands.Structure
{
	public class ArgumentStructure : IArgumentStructure
	{
		public ArgumentStructure(MethodInfo? target, 
								 MethodInfo? source, 
								 Func<object, object> converter)
		{
			Target = target;
			Source = source;
			Converter = converter;
		}

		public MethodInfo Target { get; }

		public MethodInfo Source { get; }

		public Func<object, object>? Converter { get; }
	}
}