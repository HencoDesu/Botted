using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands;

namespace Botted.Tests.TestEnvironment.Commands
{
	public class SimpleTestCommand : Command
	{
		public SimpleTestCommand(ICommandResultFactory resultFactory)
			: base("test", resultFactory)
		{ }

		
		public bool Executed { get; private set; }
		
		public override ICommandResult Execute(ICommandData? data)
		{
			Executed = true;
			return Ok("Success!");
		}
	}
}