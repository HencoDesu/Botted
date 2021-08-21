using Autofac;
using Botted.Core.Abstractions.Services.Commands.Events;
using Botted.Core.Services.Commands;
using Botted.Core.Abstractions.Services.Providers.Events;
using Botted.Tests.TestEnvironment;
using Botted.Tests.TestEnvironment.Commands;
using NUnit.Framework;

namespace Botted.Tests.CoreTests
{
	public class CommandsTest : BaseTest
	{
		[Test]
		public void SimpleCommandTest()
		{
			var command = "!test";
			var commandResult = "Success!";
			Container.BeginLifetimeScope();
			var sendEvent = Container.Resolve<NeedSendMessage>();
			var executingEvent = Container.Resolve<CommandExecuting>();
			var eventService = Container.Resolve<TestEventService>();
			var testCommand = Container.Resolve<SimpleTestCommand>();
			var commandsService = Container.Resolve<CommandService>();
			var provider = Container.Resolve<TestProvider>();
			
			provider.ReceiveMessage("!test");
			
			Assert.IsTrue(testCommand.Executed);
			Assert.IsTrue(eventService.IsRaised(sendEvent));
			Assert.IsTrue(eventService.IsRaised(executingEvent));
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
			Container.BeginLifetimeScope();
			var sendEvent = Container.Resolve<NeedSendMessage>();
			var eventService = Container.Resolve<TestEventService>();
			var testCommand = Container.Resolve<TestCommandWithData>();
			var commandsService = Container.Resolve<CommandService>();
			var provider = Container.Resolve<TestProvider>();
			
			provider.ReceiveMessage(command);
			
			Assert.IsTrue(testCommand.Executed);
			Assert.IsTrue(eventService.IsRaised(sendEvent));
			Assert.NotNull(provider.LastSentMessage);
			Assert.AreEqual(commandResult, provider.LastSentMessage.Text);
		}
	}
}