using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Commands.Abstractions.Extensions;
using Botted.Core.Plugins;

namespace Botted.Plugins.ExamplePlugin
{
	public class ExamplePlugin : BottedPlugin
	{
		public override void OnInit(IBottedBuilder bottedBuilder)
		{
			bottedBuilder.RegisterCommand<ExampleCommand, ExampleCommand.ExampleCommandData>();
		}

		public override void OnLoad(IContainer services)
		{ }
	}
}