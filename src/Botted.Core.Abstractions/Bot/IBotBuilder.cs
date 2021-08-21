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
		IBotBuilder RegisterConverters();
		IBotBuilder ReadConfig();
		IBotBuilder ConfigureDb<TDb>(Action<DbContextOptionsBuilder<TDb>>? builder = null)
			where TDb : DbContext, IBotDatabase;
	}
}