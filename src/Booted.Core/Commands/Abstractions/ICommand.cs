using System.Collections.Generic;

namespace Booted.Core.Commands.Abstractions
{
	public interface ICommand<in TData> : ICommandBase
	{
		IReadOnlyList<IArgument> Arguments { get; }

		ICommandResult Execute(TData args);
	}
}