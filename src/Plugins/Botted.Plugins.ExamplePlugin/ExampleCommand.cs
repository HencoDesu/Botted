using System.Threading.Tasks;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Commands.Abstractions.Result;

namespace Botted.Plugins.ExamplePlugin
{
	public class ExampleCommand : AbstractCommand<ExampleCommand.ExampleCommandData>
	{
		public class ExampleCommandData : ICommandData
		{
			public static ICommandDataStructure Structure { get; }
				= ICommandData.GetBuilder(() => new ExampleCommandData())
							  .WithArgument(d => d.SomeText, c => c.TextArgs[0], true)
							  .Build();

			public string SomeText { get; set; } = "";
		}
		
		public ExampleCommand() : base("example")
		{ }

		public override Task<ICommandResult> Execute(ExampleCommandData data)
		{
			return Ok($"This is just a command example. {data.SomeText}");
		}
	}
}