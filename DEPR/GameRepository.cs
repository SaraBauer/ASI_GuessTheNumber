using Microsoft.EntityFrameworkCore;

namespace ASI_GuessTheNumber.Model
{
        public class GameRepository
        {
            private readonly GameDbContext _context = new();

            public void AddGame(GameResult result)
            {
                _context.GameResults.Add(result);
                _context.SaveChanges();
            }

            public List<GameResult> GetAllGames()
            {
                return _context.GameResults
                    .Include(g => g.Guesses)
                    .ToList();
            }

        public async Task<List<GameResult>> GetAllGamesAsync()
        {
            return await _context.GameResults
                .Include(g => g.Guesses)
                .OrderByDescending(g => g.PlayedAt)
                .ToListAsync();
        }
    }
}
