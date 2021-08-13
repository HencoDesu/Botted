using Botted.Core.Abstractions.Services.Users.Data;
using Microsoft.EntityFrameworkCore;

namespace Botted.Core.Abstractions.Services.Database
{
	public interface IBotDatabase
	{
		DbSet<BotUser> Users { get; }

		public void SaveChanges();
	}
}