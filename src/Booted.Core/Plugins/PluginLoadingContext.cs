using System.Collections.Generic;
using Booted.Core.Commands;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Providers;

namespace Booted.Core.Plugins
{
	public class PluginLoadingContext
	{
		private readonly List<IProvider> _providers = new();
		private readonly List<ICommandBase> _commands = new();

		public IReadOnlyList<ICommandBase> Commands => _commands;

		public IReadOnlyList<IProvider> Providers => _providers;

		public void RegisterProvider(IProvider provider)
			=> _providers.Add(provider);

		public void RegisterCommand(ICommandBase commandBase)
			=> _commands.Add(commandBase);
	}
}