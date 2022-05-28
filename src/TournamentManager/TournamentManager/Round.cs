using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class Round
{
    public int Number { get; init; }
    public List<Match> Matches { get; set; } = new List<Match>();
    public List<Competitor> Competitors { get; set; } = new List<Competitor>();
    private readonly List<BracketNode> _nodes;

    public Round(int roundNumber, List<BracketNode> nodes)
    {
        Number = roundNumber;
        _nodes = nodes;

        foreach (var node in nodes)
        {
            Matches.Add(node.Match);
            Competitors.AddRange(node.Match.Competitors);
        }
    }
    
    public bool IsFinished => GetWinners().Count == Matches.Count && Matches.Count > 0;

    public List<Competitor> GetWinners()
    {
        var winners = new List<Competitor>();
        foreach (var match in Matches)
        {
            if (match.Winner is null)
            {
                continue;
            }

            winners.Add(match.Winner);
        }

        return winners;
    }

    public void SetMatchCompetitorsFromPreviousResult()
    {
        foreach (var node in _nodes)
        {
            node.SetMatchCompetitorsFromPreviousResult();
        }
    }
}
