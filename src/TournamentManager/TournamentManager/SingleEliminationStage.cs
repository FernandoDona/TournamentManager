using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class SingleEliminationStage : IStage
{
    public List<Competitor> Competitors { get; } = new List<Competitor>();


    public void CreateMatches(List<Competitor> listOfCompetitors, bool isOrderedBySeed)
    {
        var round = new Round();

        var numberOfMatches = listOfCompetitors.Count / 2;

        if (isOrderedBySeed)
        {
            var orderedListOfCompetitors = listOfCompetitors.OrderBy(c => c.Seed);
            var higherSeedPosition = 0;
            var lowerSeedPosition = listOfCompetitors.Count - 1;
            for (int i = 1; i <= numberOfMatches; i++)
            {

                var match = new Match { MatchNumber = 1, RoundNumber = round.Number };
                match.AddCompetitor(orderedListOfCompetitors.ElementAt(higherSeedPosition));
                match.AddCompetitor(orderedListOfCompetitors.ElementAt(lowerSeedPosition));
            }

        }

    }
}
