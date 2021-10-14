using Autofac;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Providers.Abstractions.Extensions;
using Telegram.Bot;

namespace Botted.Plugins.Providers.Telegram
{
	public class Plugin : IPlugin
	{
		public string Name => "Telegram Provider";

		/// <inheritdoc />
		public void OnInit(ContainerBuilder containerBuilder)
		{
			containerBuilder.UseProvider<TelegramProvider>()
							.RegisterConfiguration<TelergamConfiguration>("Telegram");
			
			containerBuilder.Register(c => new TelegramBotClient(c.Resolve<TelergamConfiguration>().AccessToken))
							.As<ITelegramBotClient>()
							.SingleInstance();
		}

		/// <inheritdoc />
		public void OnLoad(ILifetimeScope services) { }
	}
}