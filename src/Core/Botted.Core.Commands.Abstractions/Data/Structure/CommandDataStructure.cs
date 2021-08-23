using System;
using System.Collections.Generic;

namespace Botted.Core.Commands.Abstractions.Data.Structure
{
	/// <inheritdoc />
	public class CommandDataStructure : ICommandDataStructure
	{
		private readonly Func<ICommandData> _factory;

		public CommandDataStructure(IReadOnlyCollection<IArgumentStructure> arguments,
									Func<ICommandData> factory)
		{
			_factory = factory;
			Arguments = arguments;
		}

		public IReadOnlyCollection<IArgumentStructure> Arguments { get; }

		public ICommandData Empty => _factory();
	}
}