using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;

namespace Botted.Core.Plugins
{
	public abstract class BottedPlugin
	{
		public PluginMetadata? Metadata { get; internal set; }

		public virtual void OnInit(IBottedBuilder bottedBuilder)
		{ }
		
		public virtual void OnLoad(IContainer container) 
		{}
	}
}