using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class BracketStructure
{
    public BracketNode RootNode { get; init; }
    public int CountLevels { get; init; }

    public BracketStructure(int numberOfRounds)
    {
        var matchCounter = 0;
        RootNode = CreateBrackets(RootNode, numberOfRounds, ref matchCounter);
        CountLevels = CountLevelsInTheTree(RootNode);
    }

    public List<BracketNode> GetNodesOnRound(int targetRound)
    {
        var numberOfRounds = CountLevels;

        if (targetRound > numberOfRounds || targetRound <= 0)
        {
            throw new ArgumentException("Este round não existe.", nameof(targetRound));
        }

        var bracketLevel = ConvertRoundToTreeLevel(targetRound, numberOfRounds);
        var nodes = new List<BracketNode>();

        GetNodesOnLevel(nodes, RootNode, bracketLevel);

        return nodes;
    }

    private BracketNode CreateBrackets(BracketNode? rootBracket, int numberOfRounds, ref int matchCounter)
    {
        if (numberOfRounds > 0)
        {
            if (rootBracket is null)
            {
                rootBracket = new BracketNode();
            }

            rootBracket.Right = CreateBrackets(rootBracket.Right, numberOfRounds - 1, ref matchCounter);
            rootBracket.Left = CreateBrackets(rootBracket.Left, numberOfRounds - 1, ref matchCounter);
            rootBracket.Match = new Match { MatchNumber = matchCounter };
            matchCounter++;
        }

        return rootBracket;
    }

    private int ConvertRoundToTreeLevel(int targetRound, int numberOfRounds)
    {
        return numberOfRounds - (targetRound - 1);
    }

    private int CountLevelsInTheTree(BracketNode node)
    {
        if (node is null)
        {
            return 0;
        }

        return Math.Max(CountLevelsInTheTree(node.Left), CountLevelsInTheTree(node.Right)) + 1;
    }

    private void GetNodesOnLevel(List<BracketNode> levelNodes, BracketNode node, int bracketLevel)
    {
        if (bracketLevel > 1)
        {
            GetNodesOnLevel(levelNodes, node.Left, bracketLevel - 1);
            GetNodesOnLevel(levelNodes, node.Right, bracketLevel - 1);
        }
        else
        {
            levelNodes.Add(node);
        }
    }
}
