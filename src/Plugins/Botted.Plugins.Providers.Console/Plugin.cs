using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Plugins;
using Botted.Core.Providers.Abstractions.Extensions;

namespace Botted.Plugins.Providers.Console
{
	public class Plugin : BottedPlugin
	{
		public string Name => "Console Provider";

		public override void OnInit(IBottedBuilder bottedBuilder)
		{
			bottedBuilder.UseProvider<ConsoleProvider>();
		}
		
		public override void OnLoad(IContainer services)
		{ }
	}
}