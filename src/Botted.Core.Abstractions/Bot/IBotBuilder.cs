namespace Botted.Core.Abstractions.Bot
{
	public interface IBotBuilder : IBuilder<IBot>
	{
		IBotBuilder LoadPlugins();
		IBotBuilder RegisterServices();
		IBotBuilder RegisterEvents();
		IBotBuilder RegisterCommands();
		IBotBuilder RegisterFactories();
	}
}