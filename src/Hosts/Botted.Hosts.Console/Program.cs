using Botted.Core;
using Botted.Core.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

Host.CreateDefaultBuilder()
	.UseBotted<Bot>()
	.ConfigureLogging(builder => builder.AddSerilog())
	.Build()
	.Run();

void ConfigureLogger(ILoggerFactory loggerFactory)
{
	/*Log.Logger = new LoggerConfiguration().WriteTo.Console()
										  .WriteTo.File("Logs\\Log-.txt", rollingInterval: RollingInterval.Minute)
										  .CreateLogger();
	loggerFactory.AddSerilog();
	Log.Information("Logger configured");*/
}