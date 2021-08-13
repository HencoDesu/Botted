using Botted.Core.Abstractions.Data;

namespace Botted.Core.Abstractions.Services.Commands.Structure
{
	public interface ICommandStructure
	{
		ICommandData ParseData(BotMessage message);
	}
}