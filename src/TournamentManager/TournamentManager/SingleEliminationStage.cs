using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class SingleEliminationStage : IStage
{
    public List<Competitor> Competitors { get; } = new List<Competitor>();


    public List<Match> CreateMatches(List<Competitor> listOfCompetitors, bool isOrderedBySeed)
    {
        var listOfMatches = new List<Match>();
        var numberOfMatches = listOfCompetitors.Count / 2;

        if (isOrderedBySeed)
        {
            var orderedListOfCompetitors = listOfCompetitors.OrderBy(c => c.Seed);
            var higherSeedPosition = 0;
            var lowerSeedPosition = listOfCompetitors.Count - 1;
            for (int i = 1; i <= numberOfMatches; i++)
            {
                var match = new Match();
                match.AddCompetitor(orderedListOfCompetitors.ElementAt(higherSeedPosition));
                match.AddCompetitor(orderedListOfCompetitors.ElementAt(lowerSeedPosition));

                listOfMatches.Add(match);

                higherSeedPosition++;
                lowerSeedPosition--;
            }
        }

        return listOfMatches;
    }
}
