using System.Collections;
using System.Collections.Generic;
using Botted.Core.Commands;
using Botted.Core.Messaging.Data;
using Botted.Tests.CoreTests.TestData;
using FluentAssertions;
using Xunit;

namespace Botted.Tests.CoreTests
{
	public class CommandParsingTest
	{
		class TestData : IEnumerable<object[]>
		{
			public IEnumerator<object[]> GetEnumerator()
			{
				yield return new object[] { TestMessageGenerator.GenerateMessage("!test 5 abc b"), new TestCommandData(5, "abc", TestEnum.B, TestUsers.TestBottedUser) };
				yield return new object[] { TestMessageGenerator.GenerateMessage("!test 5 abc"), new TestCommandData(5, "abc", TestEnum.A, TestUsers.TestBottedUser) };
			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}

		private readonly CommandsConfiguration _configuration;

		public CommandParsingTest()
		{
			_configuration = new CommandsConfiguration()
			{
				CommandPrefix = '!'
			};
		}

		[Fact]
		public void ParseCommandName()
		{
			var parser = new CommandParser(_configuration);
			var message = TestMessageGenerator.GenerateMessage("!test");

			var result = parser.TryParseCommandName(message, out var commandName);

			result.Should().BeTrue();
			commandName.Should().Be("test");
		}

		[Fact]
		public void ParseWrongCommandName()
		{
			var parser = new CommandParser(_configuration);
			var message = TestMessageGenerator.GenerateMessage("@test");

			var result = parser.TryParseCommandName(message, out var commandName);

			result.Should().BeFalse();
			commandName.Should().BeEmpty();
		}

		[Theory]
		[ClassData(typeof(TestData))]
		public void ParseCommandData(BottedMessage testMessage, TestCommandData expectedData)
		{
			var parser = new CommandParser(_configuration);
			var structure = TestCommandData.Structure;

			var data = parser.ParseCommandData(testMessage, structure);
			
			data.Should().BeOfType<TestCommandData>();
			data.Should().Be(expectedData);
		}
	}
}