using System;
using Booted.Core.Commands;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Extensions;
using Booted.Core.Providers;
using NUnit.Framework;
using Pidgin;

namespace Botted.Core.Tests
{
	public class UnitTest1
	{
		private class CommandData
		{
			public int Int { get; set; }
			public string String { get; set; }
		}

		private ICommand<Unit> SimpleCommand { get; }
			= new CommandBuilder<Unit>("test").WithHandler(_ => CommandResult.Ok("Ok!"))
											  .Build();

		private ICommand<CommandData> CommandWithData { get; }
			= new CommandBuilder<CommandData>("test").WithArgument(d => d.Int)
													 .WithArgument(d => d.String)
													 .WithHandler(d => CommandResult.Ok($"{d.Int} {d.String}"))
													 .Build();

		private ICommand<Unit> CommandWithException { get; }
			= new CommandBuilder<Unit>("test").WithHandler(_ => throw new Exception("Error"))
											  .Build();
		
		private ICommand<Unit> CommandWithError { get; }
			= new CommandBuilder<Unit>("test").WithHandler(_ => CommandResult.Error("Error"))
											  .Build();

		[Test]
		public void SimpleCommandTest()
		{
			var commandManager = new CommandManager();
			commandManager.RegisterCommand(SimpleCommand);
			
			var commandResult = commandManager.TryExecuteCommand(new BotMessage { Text = "!test" });
			Assert.IsInstanceOf(typeof(CommandResult.OkCommandResult), commandResult);
			Assert.AreEqual("Ok!", commandResult.Text);
		}

		[Test]
		public void CommandWithDataTest()
		{
			var commandManager = new CommandManager();
			commandManager.RegisterCommand(CommandWithData);

			var commandResult = commandManager.TryExecuteCommand(new BotMessage { Text = "!test 5 a" });
			Assert.IsInstanceOf(typeof(CommandResult.OkCommandResult), commandResult);
			Assert.AreEqual("5 a", commandResult.Text);
		}
		
		[Test]
		public void CommandWithExceptionTest()
		{
			var commandManager = new CommandManager();
			commandManager.RegisterCommand(CommandWithException);

			var commandResult = commandManager.TryExecuteCommand(new BotMessage { Text = "!test" });
			Assert.IsInstanceOf(typeof(CommandResult.ErrorCommandResult), commandResult);
			Assert.AreEqual("Error", commandResult.Text);
		}

		[Test]
		public void CommandWithErrorTest()
		{
			var commandManager = new CommandManager();
			commandManager.RegisterCommand(CommandWithError);

			var commandResult = commandManager.TryExecuteCommand(new BotMessage { Text = "!test" });
			Assert.IsInstanceOf(typeof(CommandResult.ErrorCommandResult), commandResult);
			Assert.AreEqual("Error", commandResult.Text);
		}

	}
}