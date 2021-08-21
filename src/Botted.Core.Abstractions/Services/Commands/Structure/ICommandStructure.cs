using System;
using System.Collections.Generic;

namespace Botted.Core.Abstractions.Services.Commands.Structure
{
	public interface ICommandStructure
	{
		IReadOnlyList<IArgumentStructure> Arguments { get; }
		
		Func<ICommandData> DataFactory { get; }
	}
}