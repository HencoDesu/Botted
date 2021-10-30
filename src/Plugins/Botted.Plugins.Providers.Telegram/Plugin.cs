using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Messaging.Extensions;
using Botted.Core.Plugins;
using Telegram.Bot;

namespace Botted.Plugins.Providers.Telegram
{
	public class Plugin : BottedPlugin
	{
		public override void OnInit(IBottedBuilder containerBuilder)
		{
			containerBuilder.UseProvider<TelegramProvider>()
							.ConfigureContainer(ConfigureContainer);
		}

		public override void OnLoad(IContainer services) { }

		private void ConfigureContainer(IContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterConfigurationSection<TelergamConfiguration>("Telegram");
			containerBuilder.RegisterSingleton<ITelegramBotClient>(c => new TelegramBotClient(c.Resolve<TelergamConfiguration>().AccessToken));
		}
	}
}