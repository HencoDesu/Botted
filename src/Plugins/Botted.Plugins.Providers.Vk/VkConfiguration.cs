using JetBrains.Annotations;

namespace Botted.Plugins.Providers.Vk
{
	[UsedImplicitly]
	public class VkConfiguration
	{
		public string AccessToken { get; set; }
		public ulong GroupId { get; set; }
	}
}