using System;
using System.Linq.Expressions;
using Booted.Core.Abstractions;
using Booted.Core.Providers;
using Pidgin;

namespace Booted.Core.Commands.Abstractions
{
	public interface ICommandBuilder<TData> : IBuilder<ICommand<TData>>
	{
		ICommandBuilder<TData> WithProviderLimitation(ProviderIdentifier allowedProviders);

		ICommandBuilder<TData> WithHandler(Func<TData, ICommandResult> handler);

		ICommandBuilder<TData> WithSubCommand(string subCommandName,
											  Action<ICommandBuilder<Unit>> commandBuilder);

		ICommandBuilder<TData> WithSubCommand<TSubData>(string subCommandName,
														Action<ICommandBuilder<TSubData>> commandBuilder)
			where TSubData : new();

		ICommandBuilder<TData> WithArgument<TArgument>(Expression<Func<TData, TArgument>> expression,
													   Func<string, TArgument> converter);
	}
}