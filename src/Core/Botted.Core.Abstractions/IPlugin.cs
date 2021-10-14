using Autofac;

namespace Botted.Core.Abstractions
{
	/// <summary>
	/// Simple abstraction for plugin
	/// </summary>
	public interface IPlugin
	{
		string Name { get; }

		void OnInit(ContainerBuilder containerBuilder);
		
		void OnLoad(ILifetimeScope services);
	}
}