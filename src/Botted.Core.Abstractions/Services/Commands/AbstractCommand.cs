using System;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Abstractions.Services.Providers;

namespace Botted.Core.Abstractions.Services.Commands
{
	public abstract class Command<TData> : ICommand
		where TData : class, ICommandData, new()
	{
		private readonly ICommandResultFactory _commandResultFactory;

		protected Command(string name, 
						  IFactory<ICommandStructureBuilder<TData>> structureBuilderFactory,
						  ICommandResultFactory commandResultFactory) 
			: this(name, ProviderIdentifier.Any, structureBuilderFactory, commandResultFactory) 
		{}

		// ReSharper disable once VirtualMemberCallInConstructor
		protected Command(string name, 
						  ProviderIdentifier providerLimitation, 
						  IFactory<ICommandStructureBuilder<TData>> structureBuilderFactory,
						  ICommandResultFactory commandResultFactory)
		{
			_commandResultFactory = commandResultFactory;
			Name = name;
			ProviderLimitation = providerLimitation;

			var structureBuilder = structureBuilderFactory.Create();
			ConfigureStructure(structureBuilder);
			Structure = structureBuilder.Build();
		}

		public string Name { get; }

		public ProviderIdentifier ProviderLimitation { get; }

		public ICommandStructure Structure { get; }

		public ICommandResult Execute(ICommandData data)
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

		protected ICommandResult Ok(string message) 
			=> _commandResultFactory.Ok(message);

		// ReSharper disable once MemberCanBePrivate.Global
		protected ICommandResult Error(string message) 
			=> _commandResultFactory.Error(message);

		protected ICommandResult Error(Exception exception) 
			=> _commandResultFactory.Error(exception);
	}
}