using System;
using System.Collections.Generic;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Structure;

namespace Botted.Core.Services.Commands.Structure
{
	public class CommandStructure : ICommandStructure
	{
		/// <inheritdoc />
		public IReadOnlyList<IArgumentStructure> Arguments 
			=> throw new NotImplementedException();

		/// <inheritdoc />
		public Func<ICommandData> DataFactory 
			=> throw new NotImplementedException();
	}
	
	public class CommandStructure<TData> : ICommandStructure
		where TData : ICommandData, new()
	{
		private readonly List<IArgumentStructure> _arguments;

		public CommandStructure(List<IArgumentStructure> arguments)
		{
			_arguments = arguments;
		}

		public IReadOnlyList<IArgumentStructure> Arguments => _arguments;

		public Func<ICommandData> DataFactory { get; } 
			= () => new TData();
	}
}