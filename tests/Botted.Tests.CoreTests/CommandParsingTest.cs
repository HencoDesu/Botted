using System.Collections;
using System.Collections.Generic;
using Botted.Core.Commands;
using Botted.Core.Providers.Abstractions.Data;
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
				yield return new object[] { TestMessageGenerator.GenerateMessage("!test 5 abc b"), new TestCommandData(5, "abc", TestEnum.B, TestUsers.TestUser) };
				yield return new object[] { TestMessageGenerator.GenerateMessage("!test 5 abc"), new TestCommandData(5, "abc", TestEnum.A, TestUsers.TestUser) };
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

			parser.TryParseCommandName(message, out var commandName);
			commandName.Should().Be("test");
		}

		[Theory]
		[ClassData(typeof(TestData))]
		public void ParseCommandData(Message testMessage, TestCommandData expectedData)
		{
			var parser = new CommandParser(_configuration);
			var structure = TestCommandData.Structure;

			var data = parser.ParseCommandData(testMessage, structure);
			data.Should().BeOfType<TestCommandData>();
			data.Should().Be(expectedData);
		}
	}
}