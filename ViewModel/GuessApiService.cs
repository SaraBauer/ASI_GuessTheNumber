using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ASI_GuessTheNumber.ViewModel
{
    public class GuessApiService : IGuessApiService
    {
        private readonly HttpClient _httpClient;

        public GuessApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000"); // your API URL
        }

        public async Task SaveGuessAsync(int guess, bool isCorrect, DateTime timestamp)
        {
            var payload = new
            {
                guess = guess,
                correct = isCorrect,
                time = timestamp
            };

            var response = await _httpClient.PostAsJsonAsync("/api/guesses", payload);
            response.EnsureSuccessStatusCode();
        }
    }

}
