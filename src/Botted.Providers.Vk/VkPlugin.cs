using Booted.Core.Plugins;
using Booted.Core.Plugins.Abstractions;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

namespace Botted.Providers.Vk
{
	public class VkPlugin : AbstractPlugin
	{
		private readonly string _authToken;
		private IVkApi _vkApi;
		private VkProvider _vkProvider;

		public VkPlugin(string authToken)
		{
			_authToken = authToken;
		}
		
		protected override void OnLoad(PluginLoadingContext loadingContext)
		{
			_vkApi = new VkApi();
			_vkApi.Authorize(new ApiAuthParams()
			{
				AccessToken = _authToken
			});

			_vkProvider = new VkProvider(_vkApi);
			
			loadingContext.RegisterProvider(_vkProvider);
			_vkProvider.StartPolling();

			base.OnLoad(loadingContext);
		}

		protected override void OnUnload()
		{
			_vkProvider.StopPolling();

			_vkApi = null;
			_vkProvider = null;
			
			base.OnUnload();
		}
	}
}