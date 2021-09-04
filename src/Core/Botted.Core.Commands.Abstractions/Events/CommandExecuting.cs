using System.Diagnostics.CodeAnalysis;
using Botted.Core.Commands.Abstractions.Context;
using Botted.Core.Events.Abstractions.Events;

namespace Botted.Core.Commands.Abstractions.Events
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
	public class CommandExecuting : EventWithData<ICommandExecutingContext> { }
}