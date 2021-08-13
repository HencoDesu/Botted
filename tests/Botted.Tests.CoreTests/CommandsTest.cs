using Botted.Core.Factories;
using Botted.Core.Services.Commands;
using Botted.Core.Abstractions.Services.Providers.Events;
using Botted.Tests.TestEnvironment;
using Botted.Tests.TestEnvironment.Commands;
using NUnit.Framework;

namespace Botted.Tests.CoreTests
{
	public class CommandsTest
	{
		[Test]
		public void SimpleCommandTest()
		{
			var command = "!test";
			var commandResult = "Success!";
			var sendEvent = new NeedSendMessage();
			var eventService = new TestEventService(new MessageReceived(), sendEvent);
			var testCommand = new SimpleTestCommand();
			var commandsService = new CommandService(eventService, new [] {testCommand}, new CommandResultFactory());
			var provider = new TestProvider(eventService);
			
			provider.ReceiveMessage("!test");
			
			Assert.IsTrue(testCommand.Executed);
			Assert.IsTrue(eventService.IsRaised(sendEvent));
			Assert.NotNull(provider.LastSentMessage);
			Assert.AreEqual(commandResult, provider.LastSentMessage.Text);
		}
		
		[Test]
		public void CommandWithDataTest()
		{
			const int intData = 5;
			const string stringData = "items";
			var command = $"!test {intData} {stringData}";
			var commandResult = $"{intData} {stringData}";
			var sendEvent = new NeedSendMessage();
			var eventService = new TestEventService(new MessageReceived(), sendEvent);
			var testCommand = new TestCommandWithData();
			var commandsService = new CommandService(eventService, new [] {testCommand}, new CommandResultFactory());
			var provider = new TestProvider(eventService);
			
			provider.ReceiveMessage(command);
			
			Assert.IsTrue(testCommand.Executed);
			Assert.IsTrue(eventService.IsRaised(sendEvent));
			Assert.NotNull(provider.LastSentMessage);
			Assert.AreEqual(commandResult, provider.LastSentMessage.Text);
		}
	}
}