using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ASI_GuessTheNumber.ViewModel
{
    using Microsoft.Extensions.Logging;
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

        public async Task<bool> IsApiAvailableAsync()
        {
            try
            {
                using var client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(1)
                };
                var response = await client.GetAsync("https://localhost:7066/");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
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
            try
            {
                var response = await _http.PostAsJsonAsync("/api/game", dto);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<CreateGameResponse>();
                return result.GameId;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
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
            try
            {
                var response = await _http.PostAsJsonAsync("/api/guess", dto);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        // final call after successful game adding the attempts and the time taken
        public async Task FinalizeGameAsync(int gameId, int attempts, TimeSpan timeTaken)
        {
            var dto = new
            {
                attempts = attempts,
                timeTaken = timeTaken
            };
            try
            {
                var response = await _http.PutAsJsonAsync($"/api/game/{gameId}", dto);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private class CreateGameResponse
        {
            public int GameId { get; set; }
        }
    }


}
