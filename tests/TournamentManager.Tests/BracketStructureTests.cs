using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TournamentManager.Tests;
public class BracketStructureTests
{
    [Theory]
    [InlineData(1, 8)]
    [InlineData(3, 8)]
    [InlineData(8, 8)]
    [InlineData(2, 3)]
    public void GivenARoundNumberShouldReturnCorrectNumbersOfNodesInTheRound(int targetRound, int numberOfRounds)
    {
        var convertedRoundNumberToTreeLevel = numberOfRounds - (targetRound - 1);
        var structure = new BracketStructure(numberOfRounds);
        var expectedNumberOfNodes = Math.Pow(2, convertedRoundNumberToTreeLevel - 1);

        var nodes = structure.GetNodesOnRound(targetRound);

        Assert.Equal(expectedNumberOfNodes, nodes.Count);
    }

    [Fact]
    public void ShouldReturnAllRoundsOfTheStructure()
    {
        var numberOfRounds = 5;
        var structure = new BracketStructure(numberOfRounds);
        
        var rounds = structure.GetRounds();

        Assert.Equal(numberOfRounds, rounds.Count);
    }

    [Fact]
    public void ShouldSetNextMatchesCompetitorsFromPreviousRoundResult()
    {
        var competitors = HelperTests.GetListOfCompetitors(4);
        var stage = new SingleEliminationStage();
        stage.CreateStructure(competitors);
        var round = stage.GetCurrentRound();
        foreach (var match in round?.Matches)
        {
            match.SetWinner(match.Competitors.First());
        }

        var winners = round.Matches.Select(match => match.Winner);
        
        var currentRound = stage.GetCurrentRound();
        stage.SetNextRoundMatches(currentRound);

        var curentCompetitors = currentRound.Matches.SelectMany(match => match.Competitors);
        Assert.True(winners.SequenceEqual(curentCompetitors));
    }
}
