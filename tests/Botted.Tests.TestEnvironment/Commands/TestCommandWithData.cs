using Botted.Core.Factories;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Structure;

namespace Botted.Tests.TestEnvironment.Commands
{
	public class TestCommandWithData : Command<TestCommandWithData.TestData>
	{
		public class TestData : ICommandData
		{
			public int IntData { get; set; }
			public string StringData { get; set; }
		}

		public TestCommandWithData()
			: base("test",
				   new CommandResultFactory(),
				   new CommandStructureBuilderFactory<TestData>())
		{ }

		public bool Executed { get; private set; }

		public override ICommandResult Execute(TestData data)
		{
			Executed = true;
			return Ok($"{data.IntData} {data.StringData}");
		}

		protected override void ConfigureStructure(ICommandStructureBuilder<TestData> builder)
		{
			builder.WithArgument(d => d.IntData)
				   .WithArgument(d => d.StringData);
		}
	}
}