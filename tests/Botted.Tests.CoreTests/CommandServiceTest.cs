using System;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Commands;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Events;
using Botted.Core.Events.Abstractions;
using Botted.Core.Messaging.Events;
using Botted.Tests.CoreTests.TestData;
using FluentAssertions;
using Xunit;

namespace Botted.Tests.CoreTests
{
	public class CommandServiceTest
	{
		private readonly IEventBottedService _eventBottedService;
		private readonly ICommandParser _commandParser;

		public CommandServiceTest()
		{
			_eventBottedService = new EventBottedService();
			_commandParser = new CommandParser(
				new CommandsConfiguration 
				{ 
					CommandPrefix = '!' 
				});
		}

		[Fact]
		public void CommandSuccessExecutionTest()
		{
			var commandService = CreateCommandService();
			var command = CreateAndRegisterCommand(commandService);

			Action act = () => ReceiveMessage("!test");

			act.Should().NotThrow();
			command.Executed.Should().BeTrue();
		}
		
		[Fact]
		public void WrongCommandExecutionTest()
		{
			var commandService = CreateCommandService();
			var command = CreateAndRegisterCommand(commandService);

			Action act = () => ReceiveMessage("!tests");

			act.Should().Throw<Exception>();
			command.Executed.Should().BeFalse();
		}
		
		[Fact]
		public void NotACommandExecutionTest()
		{
			var commandService = CreateCommandService();
			var command = CreateAndRegisterCommand(commandService);

			Action act = () => ReceiveMessage("test");
			
			act.Should().NotThrow();
			command.Executed.Should().BeFalse();
		}

		[Fact]
		public void MultiplyRegistrationsTest()
		{
			var commandService = CreateCommandService();
			var command = new TestCommand();
			
			Action act = () => commandService.RegisterCommand(command);

			act.Should().NotThrow();
			act.Should().Throw<Exception>();
		}

		private ICommandBottedService CreateCommandService()
		{
			return new CommandBottedService(_eventBottedService, _commandParser);
		}

		private void ReceiveMessage(string message)
		{
			var generatedMessage = TestMessageGenerator.GenerateMessage(message);
			_eventBottedService.GetEvent<MessageReceived>()
							   .Raise(generatedMessage);
		}

		private TestCommand CreateAndRegisterCommand(ICommandBottedService commandBottedService)
		{
			return new TestCommand().Apply(commandBottedService.RegisterCommand);
		}
	}
}