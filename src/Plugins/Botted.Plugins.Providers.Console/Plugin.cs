using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Providers.Abstractions.Extensions;

namespace Botted.Plugins.Providers.Console
{
	public class Plugin : IPlugin
	{
		public string Name => "Console Provider";

		public void OnInit(IBottedBuilder bottedBuilder)
		{
			bottedBuilder.UseProvider<ConsoleProvider>();
		}
		
		public void OnLoad(IContainer services)
		{ }
	}
}