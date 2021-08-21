using System;
using System.Diagnostics.CodeAnalysis;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Abstractions.Services.Providers;
using NLog;

namespace Botted.Core.Abstractions.Services.Commands
{
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
	public abstract class CommandWithData<TData> : Command
		where TData : class, ICommandData, new()
	{
		protected CommandWithData(string name,
						  ICommandResultFactory commandResultFactory,
						  ICommandStructureBuilderFactory structureBuilderFactory, 
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
		protected CommandWithData(string name,
						  ProviderIdentifier providerLimitation,
						  ICommandResultFactory commandResultFactory,
						  ICommandStructureBuilderFactory structureBuilderFactory,
						  ILogger logger)
			: base(name, 
				   providerLimitation, 
				   commandResultFactory)
		{
			Logger = logger;
			
			var structureBuilder = structureBuilderFactory.Create<TData>();
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