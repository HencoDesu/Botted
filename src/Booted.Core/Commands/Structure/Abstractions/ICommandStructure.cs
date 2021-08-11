using Booted.Core.Commands.Abstractions;
using Booted.Core.Events.EventData;

namespace Booted.Core.Commands.Structure.Abstractions
{
	public interface ICommandStructure
	{
		ICommandData ParseData(BotMessage message);
	}
}