using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TournamentManager.Tests;
public class SingleEliminationStageTests
{
    [Theory]
    [InlineData(4)]
    [InlineData(8)]
    [InlineData(16)]
    public void MatchCompetitorsShouldBeOrganizedBySeed(int numberOfCompetitors)
    {
        var listOfCompetitors = GetListOfCompetitors(numberOfCompetitors);

        var stage = new SingleEliminationStage();
        var isOrderedBySeed = true;

        var matches = stage.CreateMatches(listOfCompetitors, isOrderedBySeed);

        var highestSeed = 1;
        var middleSeed = numberOfCompetitors / 2;
        var highestSeedOpponent = numberOfCompetitors;
        var middleSeedOpponent = middleSeed + 1;

        //errado trocar
        var highestSeedMatch = matches.SelectMany(m => m.Competitors).Where(c => c.Seed == highestSeed);
        var middleSeedMatch = matches.SelectMany(m => m.Competitors).Where(c => c.Seed == middleSeed);
        
        Assert.Equal(highestSeedOpponent, highestSeedMatch.Max(c => c.Seed));
        Assert.Equal(middleSeedOpponent, highestSeedMatch.Max(c => c.Seed));
    }

    [Theory]
    [InlineData(4)]
    [InlineData(8)]
    [InlineData(16)]
    public void IfNumberOfCompetitorsIsPowerOf2ShouldDistributeThemInMatches(int numberOfCompetitors)
    {

    }

    private List<Competitor> GetListOfCompetitors(int numberOfCompetitors)
    {
        var listOfCompetitors = new List<Competitor>();
        for (int i = 1; i <= numberOfCompetitors; i++)
        {
            listOfCompetitors.Add(new Competitor($"Competitor-{i}", i));
        }

        return listOfCompetitors;
    }
}
