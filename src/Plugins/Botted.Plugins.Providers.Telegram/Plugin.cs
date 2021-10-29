using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Providers.Abstractions.Extensions;
using Telegram.Bot;

namespace Botted.Plugins.Providers.Telegram
{
	public class Plugin : IPlugin
	{
		public string Name => "Telegram Provider";

		/// <inheritdoc />
		public void OnInit(IBottedBuilder containerBuilder)
		{
			containerBuilder.UseProvider<TelegramProvider>()
							.ConfigureContainer(ConfigureContainer);
		}

		/// <inheritdoc />
		public void OnLoad(IContainer services) { }

		private void ConfigureContainer(IContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterConfigurationSection<TelergamConfiguration>("Telegram");
			containerBuilder.RegisterSingleton<ITelegramBotClient>(c => new TelegramBotClient(c.Resolve<TelergamConfiguration>().AccessToken));
		}
	}
}