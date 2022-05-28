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
        var matchCounter = 1;
        RootNode = CreateBrackets(RootNode, numberOfRounds, ref matchCounter);
        CountLevels = CountLevelsInTheTree(RootNode);
    }

    public List<Round> GetRounds()
    {
        var rounds = new List<Round>();

        for (int i = CountLevels; i > 0; i--)
        {
            var round = GetRound(i);
            rounds.Add(round);
        }

        return rounds;
    }

    public Round GetRound(int targetRound)
    {
        if (targetRound > CountLevels || targetRound < 1)
        {
            throw new ArgumentException($"Round {targetRound} é inexistente no torneio.", nameof(targetRound));
        }

        var nodes = GetNodesOnRound(targetRound);
        var round = new Round(targetRound, nodes);

        return round;
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

    public void SetNextRoundMatches(Round? currentRound)
    {
        if (currentRound is null) return;

        currentRound.SetMatchCompetitorsFromPreviousResult();
    }

    private BracketNode CreateBrackets(BracketNode? rootBracket, int numberOfRounds, ref int matchCounter)
    {
        if (numberOfRounds > 0)
        {
            if (rootBracket is null)
            {
                rootBracket = new BracketNode();
            }

            rootBracket.Left = CreateBrackets(rootBracket.Left, numberOfRounds - 1, ref matchCounter);
            rootBracket.Right = CreateBrackets(rootBracket.Right, numberOfRounds - 1, ref matchCounter);
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
    /// <summary>
    /// Obtem o round em andamento, ou seja, que não foi finalizado.
    /// </summary>
    /// <returns>Retorna o round em andamento ou null se não existirem rounds em andamento.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Round? GetCurrentRound()
    {
        if (CountLevels < 1)
        {
            throw new InvalidOperationException("Não existem rounds na estrutura.");
        }

        Round round = null;
        
        for(int i = 1; i <= CountLevels; i++)
		{
            round = GetRound(i);
            
            if (round.IsFinished)
            {
                continue;
            }

            break;
        }
        
        return round.IsFinished ? null : round;
    }

    public void PlaceCompetitorsInInitialMatches(List<Competitor> competitors)
    {
        PopulateLeafNodes(new Queue<Competitor>(competitors), RootNode);
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

        PopulateLeafNodes(competitors, node.Left);
        PopulateLeafNodes(competitors, node.Right);
    }

}
