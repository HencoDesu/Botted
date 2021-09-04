using System.Threading.Tasks;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Result;

namespace Botted.Plugins.ExamplePlugin
{
	public class ExampleCommand : AbstractCommand
	{
		public ExampleCommand() : base("example")
		{ }

		public override Task<ICommandResult> Execute()
		{
			return Ok("This is just a command example");
		}
	}
}