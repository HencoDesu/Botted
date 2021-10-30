using System;
using Botted.Core.Abstractions.Dependencies;

namespace Botted.Core.Abstractions
{
	/// <summary>
	/// Configures Botted to use
	/// </summary>
	public interface IBottedBuilder
	{
		/// <summary>
		/// Define a folder to load plugins from
		/// </summary>
		/// <param name="pluginsPath">Plugins folder's relative path</param>
		/// <returns>Current <see cref="IBottedBuilder"/></returns>
		IBottedBuilder UsePluginsFolder(string pluginsPath);
		
		/// <summary>
		/// Define a folder containing all configs
		/// </summary>
		/// <param name="configsPath">Configs folder's relative path</param>
		/// <returns>Current <see cref="IBottedBuilder"/></returns>
		IBottedBuilder UseConfigsFolder(string configsPath);

		/// <summary>
		/// Allows to add any dependencies to DI container
		/// </summary>
		/// <param name="configure">Delegate to configure container</param>
		/// <returns>Current <see cref="IBottedBuilder"/></returns>
		IBottedBuilder ConfigureServices(Action<IContainerBuilder> configure);
	}
}