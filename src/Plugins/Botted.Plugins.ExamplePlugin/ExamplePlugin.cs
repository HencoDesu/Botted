using Botted.Core.Abstractions.Builders;
using Botted.Core.Commands.Abstractions.Extensions;
using Botted.Core.Plugins.Abstractions;

namespace Botted.Plugins.ExamplePlugin
{
	public class ExamplePlugin : IPlugin
	{
		public string Name => "ExamplePlugin";

		public void OnLoad(IBotBuilder botBuilder)
		{
			botBuilder.RegisterCommand<ExampleCommand, ExampleCommand.ExampleCommandData>();
		}
	}
}