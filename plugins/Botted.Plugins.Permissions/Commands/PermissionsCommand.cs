using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Commands.Abstractions.Result;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Plugins.Permissions.Commands
{
	public class PermissionsCommand : AbstractCommand<PermissionsCommand.PermissionsCommandData>
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
			public static ICommandDataStructure Structure { get; }
				= ICommandData.GetBuilder(() => new PermissionsCommandData())
							  .WithArgument(d => d.Mode, c => c.TextArgs[0])
							  .WithArgument(d => d.Permission, c => c.TextArgs[1])
							  .WithArgument(d => d.User, c => c.User)
							  .Build();
			
			public CommandMode Mode { get; set; }

			public string Permission { get; set; }

			public User User { get; set; }
		}
		
		/// <inheritdoc />
		public PermissionsCommand() : base("permissions")
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