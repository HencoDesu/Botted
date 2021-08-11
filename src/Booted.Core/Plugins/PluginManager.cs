using System.Collections.Generic;
using Booted.Core.Plugins.Abstractions;

namespace Booted.Core.Plugins
{
	public class PluginManager : IPluginManager
	{
		private readonly List<IPlugin> _plugins = new();
		
		public void RegisterPlugin(IPlugin plugin)
		{
			_plugins.Add(plugin);
		}

		public void LoadAllPlugins(PluginLoadingContext loadingContext)
		{
			foreach (var plugin in _plugins)
			{
				LoadPlugin(plugin, loadingContext);
			}
		}

		public void LoadPlugin(IPlugin plugin, PluginLoadingContext loadingContext)
		{
			plugin.Load(loadingContext);
		}
	}
}