using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Services.Commands.Structure;

namespace Botted.Core.Factories
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