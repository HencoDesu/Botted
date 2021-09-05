using Botted.Core.Abstractions.Builders;
using Botted.Core.Plugins.Abstractions;
using Botted.Core.Providers.Abstractions.Extensions;

namespace Botted.Plugins.Providers.Console
{
	public class Plugin : IPlugin
	{
		public string Name => "Console Provider";

		public void OnLoad(IBotBuilder botBuilder)
		{
			botBuilder.UseProvider<ConsoleProvider>();
		}
	}
}