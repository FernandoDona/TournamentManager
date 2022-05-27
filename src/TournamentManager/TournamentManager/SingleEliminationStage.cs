using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class SingleEliminationStage : IStage
{
    public List<Competitor> Competitors { get; } = new();
    public BracketStructure BracketStructure { get; private set; }
    public List<Round> Rounds { get; set; } = new();

    public void CreateStructure(List<Competitor> competitors)
    {
        var numberOfRounds = (int)Math.Log2(competitors.Count);
        BracketStructure = new BracketStructure(numberOfRounds);
        
        var organizedCompetitors = SortCompetitors(competitors);
        PlaceCompetitorsInInitialMatches(BracketStructure, organizedCompetitors);
    }

    public List<Match> GetMatchesByRound(int targetRound)
    {
        return BracketStructure.GetNodesOnRound(targetRound)
            .Select(node => node.Match)
            .ToList();
    }

    private void PlaceCompetitorsInInitialMatches(BracketStructure bracketStructure, List<Competitor> competitors)
    {
        PopulateLeafNodes(new Queue<Competitor>(competitors), BracketStructure.RootNode);
    }

    private void PopulateLeafNodes(Queue<Competitor> competitors, BracketNode node)
    {
        if (node.IsLeafNode())
        {
            while (node.Match.IsAvailableToAddCompetitor())
            {
                node.Match.AddCompetitor(competitors.Dequeue());
            }
            
            return;
        }

        PopulateLeafNodes(competitors, node.Right);
        PopulateLeafNodes(competitors, node.Left);
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
}
