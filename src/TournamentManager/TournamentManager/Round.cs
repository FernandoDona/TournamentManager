using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class Round
{
    public int Number { get; set; }
    public List<Match> Matches { get; set; } = new List<Match>();
    public List<Competitor> Winners { get; set; } = new List<Competitor>();
}
