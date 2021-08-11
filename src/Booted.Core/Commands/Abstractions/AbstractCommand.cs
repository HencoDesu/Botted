using Booted.Core.Commands.Structure;
using Booted.Core.Commands.Structure.Abstractions;
using Booted.Core.Providers;

namespace Booted.Core.Commands.Abstractions
{
	public abstract class AbstractCommand<TData> : ICommand
		where TData : class, ICommandData, new()
	{
		private ICommandStructure _structure;
		protected AbstractCommand(string name) : this(name, ProviderIdentifier.Any) {}

		protected AbstractCommand(string name, ProviderIdentifier providerLimitation)
		{
			Name = name;
			ProviderLimitation = providerLimitation;
		}

		public string Name { get; }

		public ProviderIdentifier ProviderLimitation { get; }

		public abstract ICommandStructure Structure { get; }

		public ICommandResult Execute(ICommandData data)
			=> Execute(data as TData);

		public abstract ICommandResult Execute(TData data);

		protected static ICommandStructureBuilder<TData> GetBuilder()
			=> new CommandStructureBuilder<TData>();
	}
}