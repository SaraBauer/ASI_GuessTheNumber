using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI_GuessTheNumber.Model
{
    public class GameResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int Attempts { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public int Range { get; set; }
        public DateTime PlayedAt { get; set; }
        public List<GuessEntry> Guesses { get; set; } = new List<GuessEntry>();
    }
   
}
