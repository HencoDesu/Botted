using System.Diagnostics.CodeAnalysis;

namespace Botted.Core.Abstractions.Bot
{
	[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
	public interface IBotBuilder : IBuilder<IBot>
	{
		IBotBuilder LoadPlugins();
		IBotBuilder RegisterServices();
		IBotBuilder RegisterEvents();
		IBotBuilder RegisterCommands();
		IBotBuilder RegisterFactories();
	}
}