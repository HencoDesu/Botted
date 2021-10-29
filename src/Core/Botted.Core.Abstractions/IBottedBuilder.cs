using System;
using Botted.Core.Abstractions.Dependencies;

namespace Botted.Core.Abstractions
{
	public interface IBottedBuilder
	{
		IBottedBuilder UseLibrariesFolder(string librariesPath);
		IBottedBuilder UsePluginsFolder(string pluginsPath);
		IBottedBuilder UseConfigsFolder(string configsPath);

		IBottedBuilder ConfigureContainer(Action<IContainerBuilder> configureContainer);
	}
}