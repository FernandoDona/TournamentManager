using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class BracketNode
{
    public BracketNode Left { get; set; }
    public BracketNode Right { get; set; }
    public Match Match { get; set; }

    public BracketNode()
    {

    }

    public BracketNode(BracketNode left, BracketNode right, Match match)
    {
        Left = left;
        Right = right;
        Match = match;
    }

    public bool IsLeafNode() => Left is null && Right is null;
}
