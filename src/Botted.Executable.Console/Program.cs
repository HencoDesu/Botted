using System;
using Booted.Core.Bot;

namespace Botted.Executable.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var bot = new BotBuilder().UseDefaultEventService()
									  .LoadPlugins()
									  .RegisterServices()
									  .RegisterEvents()
									  .RegisterCommands()
									  .Build();
			System.Console.WriteLine("Нажмите Пробел для закрытия бота");
			while (System.Console.ReadKey().Key != ConsoleKey.Spacebar) { }
		}
	}
}