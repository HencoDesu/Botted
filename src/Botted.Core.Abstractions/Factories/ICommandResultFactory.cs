using System;
using Botted.Core.Abstractions.Services.Commands;

namespace Botted.Core.Abstractions.Factories
{
	public interface ICommandResultFactory : IFactory<ICommandResult>
	{
		ICommandResult Ok(string text);
		ICommandResult Error(string text);
		ICommandResult Error(Exception e);
	}
}