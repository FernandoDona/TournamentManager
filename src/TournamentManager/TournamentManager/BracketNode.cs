using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class BracketNode
{
    public BracketNode? Left { get; set; }
    public BracketNode? Right { get; set; }
    public Match Match { get; set; }

    public BracketNode() { }

    public BracketNode(BracketNode left, BracketNode right, Match match)
    {
        Left = left;
        Right = right;
        Match = match;
    }

    public void SetMatchCompetitorsFromPreviousResult()
    {
        if (Left is not null && Left.Match.Winner is not null)
            Match.AddCompetitor(Left.Match.Winner);
        if (Right is not null && Right.Match.Winner is not null)
            Match.AddCompetitor(Right.Match.Winner);
    }

    public bool IsLeafNode() => Left is null && Right is null;
}
