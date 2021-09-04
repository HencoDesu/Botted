using System;
using Autofac;
using Microsoft.Extensions.Configuration;
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
		/// <param name="pluginsDirectory">Directory to load plugins</param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		IBotBuilder LoadPlugins(string pluginsDirectory);

		/// <summary>
		/// Loads all .json files in specific directory as configs
		/// </summary>
		/// <param name="configsDirectory">Directory to get configs</param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		IBotBuilder LoadConfigs(string configsDirectory);

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