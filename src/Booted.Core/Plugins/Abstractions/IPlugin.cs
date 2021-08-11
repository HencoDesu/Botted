namespace Booted.Core.Plugins.Abstractions
{
	public interface IPlugin
	{
		internal void Load(PluginLoadingContext loadingContext);
		internal void Unload();
	}
}