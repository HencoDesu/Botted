using Autofac;
using Botted.Core.Abstractions;
using Botted.Core.Providers.Abstractions.Extensions;

namespace Botted.Plugins.Providers.Console
{
	public class Plugin : IPlugin
	{
		public string Name => "Console Provider";

		/// <inheritdoc />
		public void OnInit(ContainerBuilder containerBuilder)
		{
			containerBuilder.UseProvider<ConsoleProvider>();
		}

		/// <inheritdoc />
		public void OnLoad(ILifetimeScope services)
		{ }
	}
}