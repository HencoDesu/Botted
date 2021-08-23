using System;
using System.Linq.Expressions;

namespace Botted.Core.Commands.Abstractions.Data.Structure
{
	/// <summary>
	/// Builder for <see cref="ICommandDataStructure"/>
	/// </summary>
	/// <typeparam name="TData">Data type</typeparam>
	public interface ICommandDataStructureBuilder<TData>
	{
		/// <summary>
		/// Adds an argument to current <see cref="ICommandDataStructure"/>
		/// </summary>
		/// <param name="argument">Property for that argument</param>
		/// <param name="source">Source for the argument</param>
		/// <param name="optional">If true parsing errors will be ignored</param>
		/// <typeparam name="TArgument">Argument type</typeparam>
		/// <typeparam name="TSource">Source type</typeparam>
		/// <returns>Current <see cref="ICommandDataStructureBuilder{TData}"/></returns>
		ICommandDataStructureBuilder<TData> WithArgument<TArgument, TSource>(
			Expression<Func<TData, TArgument>> argument,
			Expression<Func<ICommandParsingContext, TSource>> source,
			bool optional = false);
		
		/// <summary>
		/// Builds current <see cref="ICommandDataStructure"/>
		/// </summary>
		/// <returns></returns>
		ICommandDataStructure Build();
	}
}