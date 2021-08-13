using System;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Abstractions.Services.Providers;

namespace Botted.Core.Abstractions.Services.Commands
{
	public abstract class Command : ICommand
	{
		private readonly ICommandResultFactory _commandResultFactory;

		protected Command(string name,
						  ICommandResultFactory commandResultFactory)
			: this(name, ProviderIdentifier.Any, commandResultFactory)
		{ }

		protected Command(string name,
						  ProviderIdentifier providerLimitation,
						  ICommandResultFactory commandResultFactory)
		{
			Name = name;
			ProviderLimitation = providerLimitation;
			_commandResultFactory = commandResultFactory;
		}

		public string Name { get; }

		public ProviderIdentifier ProviderLimitation { get; }

		public ICommandStructure? Structure { get; protected init; }

		public abstract ICommandResult Execute(ICommandData? data);

		protected ICommandResult Ok(string message)
			=> _commandResultFactory.Ok(message);

		// ReSharper disable once MemberCanBePrivate.Global
		protected ICommandResult Error(string message)
			=> _commandResultFactory.Error(message);

		protected ICommandResult Error(Exception exception)
			=> _commandResultFactory.Error(exception);
	}

	public abstract class Command<TData> : Command
		where TData : class, ICommandData, new()
	{
		protected Command(string name,
						  ICommandResultFactory commandResultFactory,
						  IFactory<ICommandStructureBuilder<TData>> structureBuilderFactory)
			: this(name, ProviderIdentifier.Any, commandResultFactory, structureBuilderFactory)
		{ }

		// ReSharper disable once VirtualMemberCallInConstructor
		// ReSharper disable once MemberCanBePrivate.Global
		protected Command(string name,
						  ProviderIdentifier providerLimitation,
						  ICommandResultFactory commandResultFactory,
						  IFactory<ICommandStructureBuilder<TData>> structureBuilderFactory)
			: base(name, providerLimitation, commandResultFactory)
		{
			var structureBuilder = structureBuilderFactory.Create();
			ConfigureStructure(structureBuilder);
			Structure = structureBuilder.Build();
		}

		public override ICommandResult Execute(ICommandData? data)
		{
			try
			{
				if (data is TData commandData)
				{
					return Execute(commandData);
				}
			} catch (Exception e)
			{
				return Error(e);
			}

			return Error("Incorrect data");
		}

		// ReSharper disable once MemberCanBeProtected.Global
		public abstract ICommandResult Execute(TData data);

		protected abstract void ConfigureStructure(ICommandStructureBuilder<TData> builder);
	}
}