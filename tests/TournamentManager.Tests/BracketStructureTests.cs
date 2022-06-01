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

    [Theory]
    [InlineData(4)]
    [InlineData(16)]
    public void ShouldSetRoundMatchesFromPreviousResults(int numberOfCompetitors)
    {
        var competitors = HelperTests.GetListOfCompetitors(numberOfCompetitors);
        var stage = new SingleEliminationStage();
        stage.CreateStructure(competitors);
        var round = stage.GetCurrentRound();
        foreach (var match in round?.Matches)
        {
            match.SetWinner(match.Competitors.First());
        }

        var winners = round.Matches.Select(match => match.Winner);
        
        var currentRound = stage.GetCurrentRound();
        stage.SetRoundMatchesFromPreviousResults(currentRound);

        var curentCompetitors = currentRound.Matches.SelectMany(match => match.Competitors);
        Assert.True(winners.SequenceEqual(curentCompetitors));
    }

    [Theory]
    [InlineData(4)]
    [InlineData(16)]
    public void ShouldGetTheLastFinishedRound(int numberOfCompetitors)
    {
        var competitors = HelperTests.GetListOfCompetitors(numberOfCompetitors);
        var stage = new SingleEliminationStage();
        stage.CreateStructure(competitors);
        
        var finishedRound = stage.GetCurrentRound();
        stage.SetRoundMatchesFromPreviousResults(finishedRound);
        foreach (var match in finishedRound?.Matches)
        {
            match.SetWinner(match.Competitors.First());
        }

        finishedRound = stage.GetCurrentRound();
        stage.SetRoundMatchesFromPreviousResults(finishedRound);
        foreach (var match in finishedRound?.Matches)
        {
            match.SetWinner(match.Competitors.First());
        }

        var roundToVerify = stage.GetLastFinishedRound();

        Assert.Equal(finishedRound.Number, roundToVerify.Number);
    }

    private void FinishRound(SingleEliminationStage stage)
    {
        var currentRound = stage.GetCurrentRound();
        stage.SetRoundMatchesFromPreviousResults(currentRound);
        foreach (var match in currentRound?.Matches)
        {
            match.SetWinner(match.Competitors.First());
        }
    }
}
