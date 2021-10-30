using Botted.Core.Abstractions;
using Botted.Core.Messaging.Services;

namespace Botted.Core.Messaging.Extensions
{
	public static class BottedBuilderExtensions
	{
		public static IBottedBuilder UseProvider<TProvider>(this IBottedBuilder bottedBuilder) 
			where TProvider : IMessageProvider
		{
			return bottedBuilder.UseProvider<TProvider, TProvider>();
		}
		
		public static IBottedBuilder UseProvider<TProviderAbstraction, TProvider>(this IBottedBuilder bottedBuilder)
			where TProvider : TProviderAbstraction
			where TProviderAbstraction : IMessageProvider
		{
			return bottedBuilder.ConfigureContainer(c => c.RegisterService<TProviderAbstraction, TProvider>());
		}
	}
}