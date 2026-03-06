using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI_GuessTheNumber.Model
{
    public class GuessEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Guess { get; set; }
        public DateTime Time { get; set; }

        public int GameResultId { get; set; }
        public GameResult GameResult { get; set; }
    }
}
