using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Commands.Abstractions.Extensions;

namespace Botted.Plugins.ExamplePlugin
{
	public class ExamplePlugin : IPlugin
	{
		public string Name => "ExamplePlugin";

		/// <inheritdoc />
		public void OnInit(IBottedBuilder bottedBuilder)
		{
			bottedBuilder.RegisterCommand<ExampleCommand, ExampleCommand.ExampleCommandData>();
		}

		/// <inheritdoc />
		public void OnLoad(IContainer services)
		{ }
	}
}