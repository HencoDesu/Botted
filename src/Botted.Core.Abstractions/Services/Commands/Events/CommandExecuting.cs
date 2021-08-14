using Botted.Core.Abstractions.Services.Commands.Data;
using Botted.Core.Abstractions.Services.Events;

namespace Botted.Core.Abstractions.Services.Commands.Events
{
	public class CommandExecuting : IEvent<CommandExecuteContext> {}
}