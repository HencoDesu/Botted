using Botted.Core.Abstractions;
using Botted.Core.Commands.Extensions;
using Botted.Core.Events.Extensions;
using Botted.Core.Extensions;
using Botted.Core.Users.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;

Host.CreateDefaultBuilder(args)
	.UseSerilog(ConfigureLogger())
	.UseBotted(ConfigureBotted)
	.Build()
	.Run();

ILogger ConfigureLogger()
{
	return Log.Logger = new LoggerConfiguration().WriteTo.Console()
												 .WriteTo.File("Logs\\Log-.txt", rollingInterval: RollingInterval.Minute, retainedFileCountLimit: 5)
												 .CreateLogger();
}

void ConfigureBotted(IBottedBuilder bottedBuilder)
{
	bottedBuilder.UsePluginsFolder("Plugins")
				 .UseConfigsFolder("Configuration")
				 .UseDefaultEventService()
				 .UseDefaultCommandService()
				 .UseDefaultCommandParser()
				 .UseDefaultUserService()
				 .UseDefaultUserDatabase();
}