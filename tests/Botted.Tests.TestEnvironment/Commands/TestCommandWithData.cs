using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Structure;
using NLog;

namespace Botted.Tests.TestEnvironment.Commands
{
	public class TestCommandWithData : CommandWithData<TestCommandWithData.TestData>
	{
		public class TestData : ICommandData
		{
			public int IntData { get; set; }
			public string StringData { get; set; }
		}

		public TestCommandWithData(
			ICommandResultFactory resultFactory,
			ICommandStructureBuilderFactory structureBuilderFactory)
			: base("test",
				   resultFactory,
				   structureBuilderFactory, 
				   LogManager.CreateNullLogger())
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