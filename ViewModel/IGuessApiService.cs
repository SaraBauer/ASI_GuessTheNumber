using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI_GuessTheNumber.ViewModel
{
    public interface IGuessApiService
    {
        Task SaveGuessAsync(int guess, bool isCorrect, DateTime timestamp);
    }

}
