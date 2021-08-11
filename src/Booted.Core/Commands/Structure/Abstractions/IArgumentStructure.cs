using Booted.Core.Commands.Abstractions;

namespace Booted.Core.Commands.Structure.Abstractions
{
	public interface IArgumentStructure
	{
		void PopulateValue(ICommandData data, string value);
	}
}