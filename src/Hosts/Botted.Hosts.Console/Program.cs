using Botted.Core;
using Botted.Core.Commands.Extensions;
using Botted.Core.Events.Extensions;
using Botted.Core.Extensions;
using Botted.Core.Users.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console()
									  .WriteTo.File("Logs\\Log-.txt", rollingInterval: RollingInterval.Minute)
									  .CreateLogger();

Host.CreateDefaultBuilder()
	.UseBotted<Bot>(bottedBuilder =>
	{
		bottedBuilder.UseLibrariesFolder("Libs")
					 .UsePluginsFolder("Plugins")
					 .UseConfigsFolder("Configuration")
					 .UseDefaultEventService()
					 .UseDefaultCommandService()
					 .UseDefaultCommandParser()
					 .UseDefaultUserService()
					 .UseDefaultUserDatabase();
	})
	.Build()
	.Run();