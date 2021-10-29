using Botted.Core.Abstractions;

namespace Botted.Core.Providers.Abstractions.Extensions
{
	public static class BottedBuilderExtensions
	{
		/// <summary>
		/// Register <see cref="IProviderService"/> to the bot
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TProvider">Provider</typeparam>
		/// <returns>Current <see cref="IBottedBuilder"/></returns>
		public static IBottedBuilder UseProvider<TProvider>(this IBottedBuilder builder)
			where TProvider : IProviderService
		{
			builder.ConfigureContainer(c => c.RegisterService<TProvider>());
			return builder;
		}
	}
}