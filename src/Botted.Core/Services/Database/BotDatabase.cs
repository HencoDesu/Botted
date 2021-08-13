using Botted.Core.Abstractions.Services.Users.Data;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Services.Database
{
	public class BotDatabase : DbContext
	{
		public DbSet<BotUser> Users { get; set; }

		public BotDatabase(DbContextOptions<BotDatabase> options) 
			: base(options)
		{ }
	}
}