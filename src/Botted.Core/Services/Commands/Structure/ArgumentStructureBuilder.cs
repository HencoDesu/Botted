using System;
using System.Reflection;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Structure;

namespace Botted.Core.Services.Commands.Structure
{
	public class ArgumentStructureBuilder<TCommandData, TArgument, TSource> 
		: IArgumentStructureBuilder<TCommandData, TArgument, TSource> 
		where TCommandData : ICommandData
	{
		private MethodInfo? _targetMethod;
		private MethodInfo? _sourceMethod;
		private Func<TSource, TArgument> _converter;

		public IArgumentStructureBuilder<TCommandData, TArgument, TSource> WithSource(MethodInfo source)
		{
			_sourceMethod = source;
			return this;
		}

		public IArgumentStructureBuilder<TCommandData, TArgument, TSource> WithTarget(MethodInfo target)
		{
			_targetMethod = target;
			return this;
		}

		public IArgumentStructureBuilder<TCommandData, TArgument, TSource> WithConverter(Func<TSource, TArgument> converter)
		{
			_converter = converter;
			return this;
		}
		
		public IArgumentStructure Build()
		{
			return new ArgumentStructure(_targetMethod, _sourceMethod, d => _converter((TSource) d)!);
		}
	}
}