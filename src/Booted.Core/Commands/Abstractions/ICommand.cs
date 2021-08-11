using Booted.Core.Commands.Structure.Abstractions;
using Booted.Core.Providers;

namespace Booted.Core.Commands.Abstractions
{
	public interface ICommand
	{
		string Name { get; }
		
		ProviderIdentifier ProviderLimitation { get; }
		
		ICommandStructure Structure { get; }
		
		ICommandResult Execute(ICommandData args);
	}
}