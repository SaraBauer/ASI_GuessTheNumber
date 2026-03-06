using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ASI_GuessTheNumber.ViewModel
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class GuessApiService: IGuessApiService
    {
        private readonly HttpClient _http;

        public GuessApiService(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://localhost:7066"); // APIService URL
        }

        // The call to send a new game to the API
        public async Task<int> CreateGameAsync(int range, int targetNumber)
        {
            var dto = new
            {
                range,
                targetNumber,
                playedAt = DateTime.Now
            };

            var response = await _http.PostAsJsonAsync("/api/game", dto);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CreateGameResponse>();
            return result.GameId;
        }

        // Call to send a new a guess including the gameId for FK relationship
        public async Task SendGuessAsync(int gameId, int guess)
        {
            var dto = new
            {
                guess = guess,
                time = DateTime.Now,
                gameResultId = gameId
            };

            var response = await _http.PostAsJsonAsync("/api/guess", dto);
            response.EnsureSuccessStatusCode();
        }

        // final call after successful game adding the attempts and the time taken
        public async Task FinalizeGameAsync(int gameId, int attempts, TimeSpan timeTaken)
        {
            var dto = new
            {
                attempts = attempts,
                timeTaken = timeTaken
            };

            var response = await _http.PutAsJsonAsync($"/api/game/{gameId}", dto);
            response.EnsureSuccessStatusCode();
        }

        private class CreateGameResponse
        {
            public int GameId { get; set; }
        }
    }


}
