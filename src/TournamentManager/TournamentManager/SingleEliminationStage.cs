using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class SingleEliminationStage : IStage
{
    private BracketStructure _bracketStructure;

    public void CreateStructure(List<Competitor> competitors)
    {
        var numberOfRounds = (int)Math.Log2(competitors.Count);
        _bracketStructure = new BracketStructure(numberOfRounds);
        
        var organizedCompetitors = SortCompetitors(competitors);
        _bracketStructure.PlaceCompetitorsInInitialMatches(organizedCompetitors);
    }

    public List<Round> GetRounds()
    {
        return _bracketStructure.GetRounds();
    }

    public Round GetRound(int targetRound)
    {
        return _bracketStructure.GetRound(targetRound);
    }
    public Round? GetCurrentRound()
    {
        return _bracketStructure.GetCurrentRound();
    }
    public Round? GetLastFinishedRound()
    {
        return _bracketStructure.GetLastFinishedRound();
    }

    public List<Match> GetMatchesByRound(int targetRound)
    {
        return _bracketStructure.GetNodesOnRound(targetRound)
            .Select(node => node.Match)
            .ToList();
    }

    private List<Competitor> SortCompetitors(List<Competitor> competitors)
    {
        return SortCompetitorBySeed(competitors);
    }

    private List<Competitor> SortCompetitorBySeed(List<Competitor> competitors)
    {
        var sortedCompetitors = competitors.OrderBy(c => c.Seed).ToList();

        var rounds = Math.Log2(competitors.Count);

        var tempList = new List<Competitor>();
        for (int i = 0; i < rounds - 1; i++)
        {
            var sliceSize = (int)Math.Pow(2, i);

            while (sortedCompetitors.Count > 0)
            {
                tempList.AddRange(sortedCompetitors.GetRange(0, sliceSize));
                sortedCompetitors.RemoveRange(0, sliceSize);
                tempList.AddRange(sortedCompetitors.GetRange(sortedCompetitors.Count - sliceSize, sliceSize));
                sortedCompetitors.RemoveRange(sortedCompetitors.Count - sliceSize, sliceSize);
            }

            sortedCompetitors = tempList.GetRange(0, tempList.Count);
            tempList.Clear();
        }

        return sortedCompetitors;
    }

    public void SetRoundMatchesFromPreviousResults(Round? currentRound)
    {
        _bracketStructure.SetRoundMatchesFromPreviousResults(currentRound);
    }

}
