namespace Booted.Core.Plugins.Abstractions
{
	public interface IPluginManager
	{
		void RegisterPlugin(IPlugin plugin);
		void LoadAllPlugins(PluginLoadingContext loadingContext);
		void LoadPlugin(IPlugin plugin, PluginLoadingContext loadingContext);
	}
}