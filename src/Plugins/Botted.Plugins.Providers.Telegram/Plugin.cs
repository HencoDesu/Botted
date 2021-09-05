using Autofac;
using Botted.Core.Abstractions.Builders;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Plugins.Abstractions;
using Botted.Core.Providers.Abstractions.Extensions;
using Serilog;
using Telegram.Bot;

namespace Botted.Plugins.Providers.Telegram
{
	public class Plugin : IPlugin
	{
		public string Name => "Telegram Provider";

		public void OnLoad(IBotBuilder botBuilder)
		{
			botBuilder.UseProvider<TelegramProvider>()
					  .RegisterConfiguration<TelergamConfiguration>("Telegram")
					  .ConfigureServices(ConfigureServices);
		}

		private void ConfigureServices(ContainerBuilder builder)
		{
			builder.Register(c => new TelegramBotClient(c.Resolve<TelergamConfiguration>().AccessToken))
				   .As<ITelegramBotClient>()
				   .SingleInstance();
		}
	}
}