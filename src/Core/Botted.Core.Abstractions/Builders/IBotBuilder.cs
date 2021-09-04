using System;
using Autofac;
using Microsoft.Extensions.Logging;

namespace Botted.Core.Abstractions.Builders
{
	/// <summary>
	/// Builder for <see cref="IBot"/>
	/// </summary>
	public interface IBotBuilder : IBuilder<IBot>
	{
		/// <summary>
		/// Loads all plugins from specific directory
		/// </summary>
		/// <param name="pluginsDirectory"></param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		IBotBuilder LoadPlugins(string pluginsDirectory);

		/// <summary>
		/// Loads specific config
		/// </summary>
		/// <param name="configFileName">Config file to load</param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		IBotBuilder LoadConfig(string configFileName);

		/// <summary>
		/// Configures logger for bot
		/// </summary>
		/// <param name="configure">Action to configure logger</param>
		/// <returns>Current <see cref="IBotBuilder"/>></returns>
		IBotBuilder ConfigureLogger(Action<ILoggerFactory> configure);
		
		/// <summary>
		/// Configure services before bot started
		/// </summary>
		/// <param name="configure">Action that allows to configure services</param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		IBotBuilder ConfigureServices(Action<ContainerBuilder> configure);
	}
}