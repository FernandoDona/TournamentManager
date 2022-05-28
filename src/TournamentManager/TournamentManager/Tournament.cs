using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class Tournament
{
    public string Name { get; set; }
    public Competitor Winner { get; set; }
    public IStage Stage { get; set; }
    public List<Competitor> Competitors { get; set; }

    public Tournament(IStage stage)
    {
        Stage = stage;
        Competitors = new List<Competitor>();
    }

    public List<Round> GetRounds()
    {
        return Stage.GetRounds();
    }

    public Round GetRound(int targetRound)
    {
        return Stage.GetRound(targetRound);
    }
}
