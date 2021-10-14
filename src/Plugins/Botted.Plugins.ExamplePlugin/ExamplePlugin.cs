using Autofac;
using Botted.Core.Abstractions;
using Botted.Core.Commands.Abstractions.Extensions;

namespace Botted.Plugins.ExamplePlugin
{
	public class ExamplePlugin : IPlugin
	{
		public string Name => "ExamplePlugin";

		/// <inheritdoc />
		public void OnInit(ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterCommand<ExampleCommand, ExampleCommand.ExampleCommandData>();
		}

		/// <inheritdoc />
		public void OnLoad(ILifetimeScope services)
		{ }
	}
}