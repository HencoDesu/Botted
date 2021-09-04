using Botted.Core.Abstractions.Builders;

namespace Botted.Core.Plugins.Abstractions
{
	/// <summary>
	/// Simple abstraction for plugin
	/// </summary>
	public interface IPlugin
	{
		string Name { get; }
		
		void OnLoad(IBotBuilder botBuilder);
	}
}