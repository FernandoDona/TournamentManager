using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public interface IStage
{
    public List<Round> GetRounds();
    public Round GetRound(int targetRound);
    public Round? GetCurrentRound();
    public void SetNextRoundMatches(Round? currentRound);
}
