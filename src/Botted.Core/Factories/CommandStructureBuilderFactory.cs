using Autofac;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Services.Commands.Structure;

namespace Botted.Core.Factories
{
	public class CommandStructureBuilderFactory : ICommandStructureBuilderFactory
	{
		private readonly ILifetimeScope _lifetimeScope;

		public CommandStructureBuilderFactory(ILifetimeScope lifetimeScope)
		{
			_lifetimeScope = lifetimeScope;
		}

		/// <inheritdoc />
		public ICommandStructureBuilder Create()
			=> new CommandStructureBuilder();

		/// <inheritdoc />
		ICommandStructureBuilder<TData> ICommandStructureBuilderFactory.Create<TData>() 
			=> new CommandStructureBuilder<TData>(_lifetimeScope);
	}
}