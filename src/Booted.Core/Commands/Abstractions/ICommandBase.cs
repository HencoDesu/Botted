using Booted.Core.Providers;

namespace Booted.Core.Commands.Abstractions
{
	public interface ICommandBase
	{
		string Name { get; }
		
		ProviderIdentifier AllowedProvider { get; }
	}
}