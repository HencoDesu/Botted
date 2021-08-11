using Booted.Core.Commands.Abstractions;
using Booted.Core.Plugins;
using Booted.Core.Plugins.Abstractions;

namespace Booted.Core.Bot
{
	public interface IBot
	{
		IPluginManager PluginManager { get; }
		ICommandManager CommandManager { get; }
	}
}