using Booted.Core.Factories;
using Botted.Core.Abstractions.Services.Commands;

namespace Botted.Tests.TestEnvironment.Commands
{
	public class SimpleTestCommand : Command
	{
		public SimpleTestCommand()
			: base("test", new CommandResultFactory())
		{ }

		
		public bool Executed { get; private set; }
		
		public override ICommandResult Execute(ICommandData? data)
		{
			Executed = true;
			return Ok("Success!");
		}
	}
}