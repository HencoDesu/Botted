using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Abstractions.Services.Providers;

namespace Botted.Core.Abstractions.Services.Commands
{
	public interface ICommand : IHasAdditionalData
	{
		string Name { get; }
		
		ProviderIdentifier ProviderLimitation { get; }
		
		ICommandStructure? Structure { get; }
		
		ICommandResult Execute(ICommandData? args);
	}
}