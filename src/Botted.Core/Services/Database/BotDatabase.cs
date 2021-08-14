using Botted.Core.Abstractions.Services.Database;
using Botted.Core.Abstractions.Services.Users.Data;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Services.Database
{
	public class BotDatabase : DbContext, IBotDatabase
	{
		public DbSet<BotUser> Users { get; set; } = null!;

		public BotDatabase(DbContextOptions<BotDatabase> options)
			: base(options)
		{ }

		public new void SaveChanges()
			=> base.SaveChanges();
	}
}