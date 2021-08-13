using System;
using Botted.Core.Bot;

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
									  .Build();
			System.Console.WriteLine("Нажмите Пробел для закрытия бота");
			while (System.Console.ReadKey().Key != ConsoleKey.Spacebar) { }
		}
	}
}