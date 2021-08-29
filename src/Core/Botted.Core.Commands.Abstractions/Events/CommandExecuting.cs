using Botted.Core.Commands.Abstractions.Context;
using Botted.Core.Events.Abstractions;

namespace Botted.Core.Commands.Abstractions.Events
{
	public class CommandExecuting : IEventWithData<ICommandExecutingContext> { }
}