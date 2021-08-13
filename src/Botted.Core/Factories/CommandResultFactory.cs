using System;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Result;

namespace Botted.Core.Factories
{
	public class CommandResultFactory : ICommandResultFactory
	{
		object IFactory.Create()
			=> Create();

		public ICommandResult Create()
			=> new NoneCommandResult();

		public ICommandResult Ok(string text)
			=> new OkCommandResult(text);

		public ICommandResult Error(string text)
			=> new ErrorCommandResult(text);

		public ICommandResult Error(Exception e)
			=> new ErrorCommandResult($"Error occured: {e}");
	}
}