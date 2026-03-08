using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI_GuessTheNumber.ViewModel
{
    public interface IGuessApiService
    {
        Task<int> CreateGameAsync(int range, int targetNumber);
        Task SendGuessAsync(int gameId, int guess);
        Task FinalizeGameAsync(int gameId, int attempts, TimeSpan timeTaken);
        Task<bool> IsApiAvailableAsync();
    }

}
