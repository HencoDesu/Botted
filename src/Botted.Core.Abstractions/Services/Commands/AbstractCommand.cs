using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Abstractions.Services.Providers;
using NLog;

namespace Botted.Core.Abstractions.Services.Commands
{
	public abstract class Command : ICommand
	{
		private readonly List<IAdditionalData> _additionalData = new ();
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

		public TData? GetAdditionalData<TData>()
			where TData : IAdditionalData
			=> _additionalData.OfType<TData>().SingleOrDefault();

		public void SaveAdditionalData<TData>(TData data) 
			where TData : IAdditionalData 
			=> _additionalData.Add(data);

		public abstract ICommandResult Execute(ICommandData? data);

		protected ICommandResult Ok(string message)
			=> _commandResultFactory.Ok(message);

		protected ICommandResult Error(string message)
			=> _commandResultFactory.Error(message);

		protected ICommandResult Error(Exception exception)
			=> _commandResultFactory.Error(exception);
	}

	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
	public abstract class Command<TData> : Command
		where TData : class, ICommandData, new()
	{
		protected Command(string name,
						  ICommandResultFactory commandResultFactory,
						  IFactory<ICommandStructureBuilder<TData>> structureBuilderFactory, 
						  ILogger logger)
			: this(name, 
				   ProviderIdentifier.Any, 
				   commandResultFactory, 
				   structureBuilderFactory, 
				   logger)
		{
			Logger = logger;
		}

		[SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
		protected Command(string name,
						  ProviderIdentifier providerLimitation,
						  ICommandResultFactory commandResultFactory,
						  IFactory<ICommandStructureBuilder<TData>> structureBuilderFactory,
						  ILogger logger)
			: base(name, 
				   providerLimitation, 
				   commandResultFactory)
		{
			Logger = logger;
			
			var structureBuilder = structureBuilderFactory.Create();
			ConfigureStructure(structureBuilder);
			Structure = structureBuilder.Build();
		}
		
		protected ILogger Logger { get; }

		public override ICommandResult Execute(ICommandData? data)
		{
			if (data is not TData commandData)
			{
				return Error("Incorrect data");
			}
			try
			{
				return Execute(commandData);
			} catch (Exception e)
			{
				Logger.Error(e, "Exception during command {0}", Name);
				return Error(e);
			}
		}

		public abstract ICommandResult Execute(TData data);

		protected abstract void ConfigureStructure(ICommandStructureBuilder<TData> builder);
	}
}