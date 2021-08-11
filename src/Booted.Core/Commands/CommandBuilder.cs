using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Extensions;
using Booted.Core.Providers;
using Pidgin;

namespace Booted.Core.Commands
{
	public class CommandBuilder<TData> : ICommandBuilder<TData>
	{
		private readonly string _name;
		private readonly List<ICommandBase> _subcommands = new();
		private readonly List<IArgument> _arguments = new();
		private ProviderIdentifier _allowedProvider = ProviderIdentifier.Any;
		private Func<TData, ICommandResult> _handler = (_) => null;

		public CommandBuilder(string name)
		{
			_name = name;
		}

		public ICommand<TData> Build()
			=> new Command<TData>(_name, _allowedProvider, _handler, _arguments);

		public ICommandBuilder<TData> WithProviderLimitation(ProviderIdentifier allowedProviders)
			=> this.WithField(ref _allowedProvider, allowedProviders);

		public ICommandBuilder<TData> WithHandler(Func<TData, ICommandResult> handler)
			=> this.WithField(ref _handler, handler);

		public ICommandBuilder<TData> WithSubCommand(string subCommandName, 
													 Action<ICommandBuilder<Unit>> commandBuilder)
			=> this.WithListed(_subcommands, 
							   new CommandBuilder<Unit>(subCommandName).ChainInvoke(commandBuilder)
																 .Build());

		public ICommandBuilder<TData> WithSubCommand<TSubData>(string subCommandName,
															   Action<ICommandBuilder<TSubData>> commandBuilder) 
			where TSubData : new() 
			=> this.WithListed(_subcommands, new CommandBuilder<TSubData>(subCommandName).ChainInvoke(commandBuilder)
																																																.Build());

		public ICommandBuilder<TData> WithArgument<TArgument>(Expression<Func<TData, TArgument>> expression,
															  Func<string, TArgument> converter)
			=> this.WithListed(_arguments, new Argument<TArgument>(expression.Body as MemberExpression, converter));
	}
}