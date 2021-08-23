using System;
using Botted.Core.Commands.Abstractions.Data.Structure;

namespace Botted.Core.Commands.Abstractions.Data
{
	/// <summary>
	/// Base abstraction of command data
	/// </summary>
	public interface ICommandData
	{
		/// <summary>
		/// Structure used for data parsing
		/// </summary>
		static abstract ICommandDataStructure Structure { get; }

		/// <summary>
		/// Creates a new instance of <see cref="ICommandDataStructureBuilder{TData}"/>
		/// </summary>
		/// <param name="factory">Func that creates a new instance of data</param>
		/// <typeparam name="TData">Data type</typeparam>
		/// <returns><see cref="ICommandDataStructureBuilder{TData}"/></returns>
		protected static ICommandDataStructureBuilder<TData> GetBuilder<TData>(Func<TData> factory) 
			where TData : class, ICommandData 
			=> new CommandDataStructureBuilder<TData>(factory);
	}
}