using System;
using System.Diagnostics.CodeAnalysis;
using Botted.Core.Abstractions.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Abstractions.Bot
{
	[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
	public interface IBotBuilder : IBuilder<IBot>
	{
		IBotBuilder LoadPlugins();
		IBotBuilder RegisterServices();
		IBotBuilder RegisterEvents();
		IBotBuilder RegisterCommands();
		IBotBuilder RegisterFactories();
		IBotBuilder ReadConfig();
		IBotBuilder ConfigureDb<TDb>(Action<DbContextOptionsBuilder<TDb>> builder)
			where TDb : DbContext, IBotDatabase;
	}
}