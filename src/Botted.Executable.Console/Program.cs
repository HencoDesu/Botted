using System;
using Botted.Core.Bot;
using Botted.Core.Services.Database;

namespace Botted.Executable.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var bot = new BotBuilder().LoadPlugins()
									  .RegisterServices()
									  .RegisterEvents()
									  .RegisterCommands()
									  .RegisterFactories()
									  .RegisterFactories()
									  .RegisterConverters()
									  .ReadConfig()
									  .ConfigureDb<BotDatabase>()
									  .Build();
			System.Console.WriteLine("Нажмите Пробел для закрытия бота");
			while (System.Console.ReadKey().Key != ConsoleKey.Spacebar) { }
		}
	}
}