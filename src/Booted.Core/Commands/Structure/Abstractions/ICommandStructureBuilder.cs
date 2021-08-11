using System;
using System.Linq.Expressions;
using Booted.Core.Abstractions;

namespace Booted.Core.Commands.Structure.Abstractions
{
	public interface ICommandStructureBuilder<TData> : IBuilder<ICommandStructure>
	{
		ICommandStructureBuilder<TData> WithArgument<TArgument>(
			Expression<Func<TData, TArgument>> expression,
			Func<string, TArgument> converter);
	}
}