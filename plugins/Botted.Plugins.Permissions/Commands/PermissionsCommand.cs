using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Abstractions.Services.Users.Data;
using NLog;

namespace Botted.Plugins.Permissions.Commands
{
	public class PermissionsCommand : CommandWithData<PermissionsCommand.PermissionsCommandData>
	{
		public enum CommandMode
		{
			Grant,
			Take,
			Show,
			All
		}

		public class PermissionsCommandData : ICommandData
		{
			public CommandMode Mode { get; set; }

			public string Permission { get; set; }

			public BotUser User { get; set; }
		}

		public PermissionsCommand(ICommandResultFactory commandResultFactory,
								  ICommandStructureBuilderFactory structureBuilderFactory)
			: base("permissions",
				   commandResultFactory,
				   structureBuilderFactory,
				   LogManager.GetCurrentClassLogger())
		{ }

		public override ICommandResult Execute(PermissionsCommandData data)
			=> data.Mode switch
			{
				CommandMode.Grant => OnGrantMode(data),
				CommandMode.Take => OnTakeMode(data),
				CommandMode.Show => OnShowMode(data),
				CommandMode.All => OnAllMode(data),
				_ => Error("Unknown mode")
			};

		protected override void ConfigureStructure(ICommandStructureBuilder<PermissionsCommandData> builder)
		{
			builder.WithEnumArgument(d => d.Mode, c => c.TextArgs[0])
				   .WithArgument(d => d.Permission, c => c.TextArgs[1])
				   .WithArgument(d => d.User, c => c.User);
		}

		private ICommandResult OnGrantMode(PermissionsCommandData data)
		{
			throw new System.NotImplementedException();
		}

		private ICommandResult OnTakeMode(PermissionsCommandData arg)
		{
			throw new System.NotImplementedException();
		}

		private ICommandResult OnShowMode(PermissionsCommandData arg)
		{
			throw new System.NotImplementedException();
		}

		private ICommandResult OnAllMode(PermissionsCommandData arg)
		{
			throw new System.NotImplementedException();
		}
	}
}