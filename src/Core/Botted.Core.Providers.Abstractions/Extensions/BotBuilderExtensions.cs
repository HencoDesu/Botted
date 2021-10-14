using Autofac;
using Botted.Core.Abstractions;

namespace Botted.Core.Providers.Abstractions.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Register <see cref="IProviderService"/> to the bot
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TProvider">Provider</typeparam>
		/// <returns>Current <see cref="ContainerBuilder"/></returns>
		public static ContainerBuilder UseProvider<TProvider>(this ContainerBuilder builder)
			where TProvider : IProviderService
		{
			builder.RegisterType<TProvider>()
				   .As<IProviderService>()
				   .As<IService>()
				   .SingleInstance()
				   .AutoActivate();
			return builder;
		}
	}
}