using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Structure;

namespace Botted.Core.Abstractions.Factories
{
	public interface ICommandStructureBuilderFactory : IFactory<ICommandStructureBuilder>
	{
		public ICommandStructureBuilder<TData> Create<TData>() 
			where TData : ICommandData, new();
	}
}