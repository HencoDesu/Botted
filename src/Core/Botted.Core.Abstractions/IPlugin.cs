using Botted.Core.Abstractions.Dependencies;

namespace Botted.Core.Abstractions
{
	/// <summary>
	/// Simple abstraction for plugin
	/// </summary>
	public interface IPlugin
	{
		string Name { get; }

		void OnInit(IBottedBuilder bottedBuilder);
		
		void OnLoad(IContainer services);
	}
}