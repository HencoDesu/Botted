namespace Botted.Core.Abstractions.Services.Commands.Structure
{
	public interface IArgumentStructure
	{
		void PopulateValue(ICommandData data, string value);
	}
}