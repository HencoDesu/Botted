using System;
using System.Reflection;

namespace Botted.Core.Abstractions.Services.Commands.Structure
{
	public interface IArgumentStructureBuilder<out TCommandData, in TArgument, out TSource> 
		: IBuilder<IArgumentStructure> 
		where TCommandData : ICommandData
	{
		IArgumentStructureBuilder<TCommandData, TArgument, TSource> WithSource(
			MethodInfo source);

		IArgumentStructureBuilder<TCommandData, TArgument, TSource> WithTarget(
			MethodInfo target);

		IArgumentStructureBuilder<TCommandData, TArgument, TSource> WithConverter(
			Func<TSource, TArgument> converter);
	}
}