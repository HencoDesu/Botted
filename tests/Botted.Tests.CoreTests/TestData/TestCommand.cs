using System.Threading.Tasks;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Result;

namespace Botted.Tests.CoreTests.TestData
{
	public class TestCommand : AbstractCommand
	{
		public bool Executed { get; private set; }
		
		public TestCommand() : base("test")
		{ }

		public override Task<ICommandResult> Execute()
		{
			Executed = true;
			return Ok("Executed");
		}
	}
}