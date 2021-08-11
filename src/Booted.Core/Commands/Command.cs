using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Providers;
using Pidgin;

namespace Booted.Core.Commands
{
	public class Command<TData> : ICommand<TData> 
	{
		private readonly Func<TData, ICommandResult> _handler;
		private readonly List<IArgument> _arguments;

		public Command(string name, 
							   ProviderIdentifier allowedProvider, 
							   Func<TData, ICommandResult> handler, 
							   List<IArgument> arguments)
		{
			_handler = handler;
			_arguments = arguments;
			Name = name;
			AllowedProvider = allowedProvider;
			DataParser = Parser.CommandName.SkipAtLeastOnce().Then(Parser.Argument.Many().Map(s => s.ToList())).Map(MapArguments);
		}

		public string Name { get; }

		public IReadOnlyList<IArgument> Arguments => _arguments;

		public ProviderIdentifier AllowedProvider { get; }

		public Parser<char, object> DataParser { get; }

		public ICommandResult Execute(TData args)
		{
			try
			{
				return _handler(args);
			} catch (Exception e)
			{
				return CommandResult.Error(e.Message);
			}
			
		}

		private object MapArguments(List<string> args)
		{
			try
			{
				var data = Activator.CreateInstance<TData>();
				for (var i = 0; i < args.Count; i++)
				{
					var argument = _arguments[i];
					var arg = args[i];
					var argValue = argument.Converter.Invoke(arg);
					((PropertyInfo)argument.Member.Member).SetMethod?.Invoke(data, new[] { argValue });
				}

				return data;
			} catch
			{
				return default(TData);
			}
			
		}
	}
}