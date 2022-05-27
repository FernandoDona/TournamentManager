﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TournamentManager.Tests;
public class SingleEliminationStageTests
{
    [Theory]
    [InlineData(2)]
    [InlineData(8)]
    [InlineData(32)]
    public void MatchCompetitorsShouldBeOrganizedBySeed(int numberOfCompetitors)
    {
        var listOfCompetitors = GetListOfCompetitors(numberOfCompetitors);

        var stage = new SingleEliminationStage();

        stage.CreateStructure(listOfCompetitors);
        var matches = stage.GetMatchesByRound(1);

        var highestSeed = 1;
        var middleSeed = numberOfCompetitors / 2;
        var highestSeedOpponent = numberOfCompetitors;
        var middleSeedOpponent = middleSeed + 1;

        var highestSeedMatch = matches.Where(m => m.Competitors.FirstOrDefault(c => c.Seed == highestSeed)?.Seed == highestSeed).Single();
        var middleSeedMatch = matches.Where(m => m.Competitors.FirstOrDefault(c => c.Seed == middleSeed)?.Seed == middleSeed).Single();
        
        Assert.Equal(highestSeedOpponent, highestSeedMatch.Competitors.Max(c => c.Seed));
        Assert.Equal(middleSeedOpponent, middleSeedMatch.Competitors.Max(c => c.Seed));
    }

    [Theory]
    [InlineData(4)]
    [InlineData(8)]
    [InlineData(16)]
    public void IfNumberOfCompetitorsIsPowerOf2ShouldDistributeThemInMatches(int numberOfCompetitors)
    {
        var listOfCompetitors = GetListOfCompetitors(numberOfCompetitors);
        var stage = new SingleEliminationStage();
        stage.CreateStructure(listOfCompetitors);
        
        var matches = stage.GetMatchesByRound(1);

        Assert.Equal(numberOfCompetitors / 2, matches.Count());
    }

    [Theory]
    [InlineData(4)]
    [InlineData(8)]
    [InlineData(16)]
    public void GivenANumberOfCompetitorsShouldCreateDefinedNumberOfRounds(int numberOfCompetitors)
    {
        var competitors = GetListOfCompetitors(numberOfCompetitors);
        var quantityOfRounds = Math.Log2(competitors.Count);
        var stage = new SingleEliminationStage();
        stage.CreateStructure(competitors);

        var quantityOfLevels = stage.BracketStructure.CountLevels;
        Assert.Equal(quantityOfRounds, quantityOfLevels);
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
