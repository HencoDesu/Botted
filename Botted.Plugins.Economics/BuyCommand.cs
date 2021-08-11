using Booted.Core.Commands;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Commands.Structure.Abstractions;
using Booted.Core.Extensions;

namespace Botted.Plugins.Economics
{
	public class BuyCommand : AbstractCommand<BuyCommand.BuyCommandData>
	{
		public class BuyCommandData : ICommandData
		{
			public string Name { get; set; }
			public int Amount { get; set; }
		}

		public BuyCommand() : base("buy")
		{ }

		public override ICommandStructure Structure { get; }
			= GetBuilder().WithArgument(d => d.Name)
						  .WithArgument(d => d.Amount)
						  .Build();

		public override ICommandResult Execute(BuyCommandData data)
		{
			return CommandResult.Ok($"Ты купил х{data.Amount} {data.Name}");
		}
	}
}