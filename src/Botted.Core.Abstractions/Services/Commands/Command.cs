using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Abstractions.Services.Providers;

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
}