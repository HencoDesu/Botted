using Botted.Core.Commands;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Providers.Abstractions.Data;
using FluentAssertions;
using Xunit;

namespace Botted.Tests.CoreTests
{
	public class CommandsTests
	{
		private class TestCommandData : ICommandData
		{
			public enum TestEnum
			{
				A,
				B,
				C
			}
			
			public static ICommandDataStructure Structure { get; }
				= ICommandData.GetBuilder(() => new TestCommandData())
							  .WithArgument(d => d.IntData, c => c.TextArgs[0])
							  .WithArgument(d => d.StringData, c => c.TextArgs[1])
							  .WithArgument(d => d.EnumData, c => c.TextArgs[2])
							  .Build();
			
			public int IntData { get; set; }
			
			public string StringData { get; set; }
			
			public TestEnum EnumData { get; set; }
		}

		[Fact]
		public void CommandParsingTest()
		{
			var parser = new CommandParser();
			var message = new Message {Text = "!test 5 abc b"};
			var commandStructure = TestCommandData.Structure;

			var data = parser.ParseCommandData(message, commandStructure);
			data.Should().BeOfType<TestCommandData>();
			data.As<TestCommandData>().IntData.Should().Be(5);
			data.As<TestCommandData>().StringData.Should().Be("abc");
			data.As<TestCommandData>().EnumData.Should().Be(TestCommandData.TestEnum.B);
		}
	}
}