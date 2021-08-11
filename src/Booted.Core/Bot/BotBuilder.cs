using Booted.Core.Abstractions;
using Booted.Core.Commands;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Plugins;
using Booted.Core.Plugins.Abstractions;

namespace Booted.Core.Bot
{
	public class BotBuilder : IBuilder<IBot>
	{
		private readonly IPluginManager _pluginManager = new PluginManager();
		private readonly ICommandManager _commandManager = new CommandManager();

		public BotBuilder RegisterPlugin(IPlugin plugin)
		{
			_pluginManager.RegisterPlugin(plugin);
			return this;
		}

		public IBot Build() 
			=> new Bot(_pluginManager, _commandManager);
	}
}