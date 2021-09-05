using Botted.Core;
using Botted.Core.Commands.Extensions;
using Botted.Core.Events.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;

var bot = new BotBuilder().ConfigureLogger(ConfigureLogger)
						  .UseDefaultEventService()
						  .UseDefaultCommandService()
						  .UseDefaultCommandParser()
						  .LoadConfigs("Configs")
						  .LoadPlugins("Plugins")
						  .Build();
bot.Start();

void ConfigureLogger(ILoggerFactory loggerFactory)
{
	Log.Logger = new LoggerConfiguration().WriteTo.Console()
										  .CreateLogger();
	loggerFactory.AddSerilog();
	Log.Information("Logger configured");
}