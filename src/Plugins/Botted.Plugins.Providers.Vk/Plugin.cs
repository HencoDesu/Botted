﻿using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Dependencies;
using Botted.Core.Messaging.Extensions;
using Botted.Core.Plugins;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

namespace Botted.Plugins.Providers.Vk
{
	public class Plugin : BottedPlugin
	{
		public override void OnLoad(IContainer services)
		{
			var configuration = services.Resolve<VkConfiguration>();
			
			var vkApi = services.Resolve<IVkApi>();
			vkApi.Authorize(new ApiAuthParams
			{
				AccessToken = configuration.AccessToken
			});
		}

		public override void OnInit(IBottedBuilder containerBuilder)
		{
			containerBuilder.UseProvider<VkProvider>()
							.ConfigureServices(ConfigureContainer);
		}

		private void ConfigureContainer(IContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterSingleton<IVkApi, VkApi>();
			containerBuilder.RegisterConfigurationSection<VkConfiguration>("Vk");
		}
	}
}