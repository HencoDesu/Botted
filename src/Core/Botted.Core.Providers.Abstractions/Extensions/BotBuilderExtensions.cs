using Botted.Core.Abstractions.Builders;
using Botted.Core.Abstractions.Extensions;

namespace Botted.Core.Providers.Abstractions.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Register <see cref="IProviderService"/> to the bot
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TProvider">Provider</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseProvider<TProvider>(this IBotBuilder builder)
			where TProvider : IProviderService
		{
			return builder.RegisterService<IProviderService, TProvider>();
		}
	}
}