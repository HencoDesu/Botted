using System;
using System.Reflection;

namespace Botted.Core.Abstractions.Services.Commands.Structure
{
	public interface IArgumentStructure
	{
		MethodInfo Target { get; }
		MethodInfo Source { get; }
		Func<object, object>? Converter { get; }
	}
}