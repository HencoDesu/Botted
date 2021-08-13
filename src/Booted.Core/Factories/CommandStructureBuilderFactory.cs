using Booted.Core.Services.Commands.Structure;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands;

namespace Booted.Core.Factories
{
	public class CommandStructureBuilderFactory<TData> : IFactory<CommandStructureBuilder<TData>> 
		where TData : class, ICommandData, new()
	{
		public CommandStructureBuilder<TData> Create()
			=> new ();

		object IFactory.Create() 
			=> Create();
	}
}