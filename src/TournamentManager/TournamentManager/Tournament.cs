using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class Tournament
{
    public Competitor Winner { get; set; }
    public IStage Stage { get; set; }
    public List<Competitor> Competitors { get; set; }

    public Tournament(IStage stage)
    {
        Stage = stage;
        Competitors = new List<Competitor>();
    }
}
