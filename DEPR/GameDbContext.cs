using Microsoft.EntityFrameworkCore;

namespace ASI_GuessTheNumber.Model
{

    public class GameDbContext : DbContext
    {
        public DbSet<GameResult> GameResults { get; set; }
        public DbSet<GuessEntry> GuessEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("GameDatabase");
        }
    }

}
