using System.Threading.Tasks;
using Botted.Core;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Extensions;
using Botted.Core.Commands.Abstractions.Result;
using Botted.Core.Commands.Extensions;
using Botted.Core.Events.Extensions;
using Botted.Core.Providers.Abstractions.Extensions;
using Botted.Plugins.Providers.Console;
using Microsoft.Extensions.Logging;
using Serilog;

var bot = new BotBuilder().UseProvider<ConsoleProvider>()
						  .UseDefaultEventService()
						  .UseDefaultCommandService()
						  .UseDefaultCommandParser()
						  .RegisterCommand<PingCommand, EmptyCommandData>()
						  .ConfigureLogger(ConfigureLogger)
						  .Build();
bot.Start();

void ConfigureLogger(ILoggerFactory loggerFactory)
{
	Log.Logger = new LoggerConfiguration().WriteTo.Console()
										  .CreateLogger();
	loggerFactory.AddSerilog();
	Log.Information("Logger configured");
}

class PingCommand : AbstractCommand
{
	public PingCommand() : base("ping")
	{ }
	
	public override Task<ICommandResult> Execute()
	{
		return Ok("pong");
	}
}