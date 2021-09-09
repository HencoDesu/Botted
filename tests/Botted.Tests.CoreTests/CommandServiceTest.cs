using System;
using Botted.Core.Commands;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Events;
using Botted.Core.Events.Abstractions;
using Botted.Core.Providers.Abstractions.Data;
using Botted.Core.Providers.Abstractions.Events;
using Botted.Tests.CoreTests.TestData;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Botted.Tests.CoreTests
{
	public class CommandServiceTest
	{
		private readonly CommandsConfiguration _configuration;

		public CommandServiceTest()
		{
			_configuration = new CommandsConfiguration
			{
				CommandPrefix = '!'
			};
		}

		[Fact]
		public void CommandSuccessExecutionTest()
		{
			var eventService = new EventService();
			var commandParser = new CommandParser(_configuration);
			var message = TestMessageGenerator.GenerateMessage("!test");
			var command = new TestCommand();
			var commandService = new CommandService(eventService, commandParser);
			commandService.RegisterCommand(command);

			Action act = () => eventService.Raise<MessageReceived, Message>(message);

			act.Should().NotThrow();
			command.Executed.Should().BeTrue();
		}
		
		[Fact]
		public void WrongCommandExecutionTest()
		{
			var eventService = new EventService();
			var commandParser = new CommandParser(_configuration);
			var message = TestMessageGenerator.GenerateMessage("!tests");
			var command = new TestCommand();
			var commandService = new CommandService(eventService, commandParser);
			commandService.RegisterCommand(command);

			Action act = () => eventService.Raise<MessageReceived, Message>(message);

			act.Should().Throw<Exception>();
			command.Executed.Should().BeFalse();
		}
		
		[Fact]
		public void NotACommandExecutionTest()
		{
			var eventService = new EventService();
			var commandParser = new CommandParser(_configuration);
			var message = TestMessageGenerator.GenerateMessage("test");
			var command = new TestCommand();
			var commandService = new CommandService(eventService, commandParser);
			commandService.RegisterCommand(command);

			eventService.Raise<MessageReceived, Message>(message);
			command.Executed.Should().BeFalse();
		}

		[Fact]
		public void MultiplyRegistrationsTest()
		{
			var commandService = new CommandService(A.Fake<IEventService>(), A.Fake<ICommandParser>());
			var command = new TestCommand();
			Action act = () => commandService.RegisterCommand(command);

			act.Should().NotThrow();
			act.Should().Throw<Exception>();
		}
	}
}