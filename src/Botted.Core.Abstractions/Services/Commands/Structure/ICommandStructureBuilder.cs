using System;
using System.Linq.Expressions;

namespace Botted.Core.Abstractions.Services.Commands.Structure
{
	public interface ICommandStructureBuilder<TData> : IBuilder<ICommandStructure>
	{
		ICommandStructureBuilder<TData> WithArgument<TArgument>(
			Expression<Func<TData, TArgument>> expression,
			Func<string, TArgument> converter);
	}
}