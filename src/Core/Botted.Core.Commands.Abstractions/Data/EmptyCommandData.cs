using Botted.Core.Commands.Abstractions.Data.Structure;

namespace Botted.Core.Commands.Abstractions.Data
{
	public class EmptyCommandData : ICommandData
	{
		public static ICommandDataStructure Structure { get; }
			= ICommandData.GetBuilder(() => new EmptyCommandData())
						  .Build();
	}
}