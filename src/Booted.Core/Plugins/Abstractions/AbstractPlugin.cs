using System;

namespace Booted.Core.Plugins.Abstractions
{
	public abstract class AbstractPlugin : IPlugin
	{
		public event Action Loaded;
		public event Action Unloaded;

		protected AbstractPlugin()
		{
			Loaded += () => { };
			Unloaded += () => { };
		}
		
		void IPlugin.Load(PluginLoadingContext loadingContext)
		{
			OnLoad(loadingContext);
		}

		void IPlugin.Unload()
		{
			OnUnload();
		}

		protected virtual void OnLoad(PluginLoadingContext loadingContext)
		{
			Loaded.Invoke();
		}

		protected virtual void OnUnload()
		{
			Unloaded.Invoke();
		}
	}
}