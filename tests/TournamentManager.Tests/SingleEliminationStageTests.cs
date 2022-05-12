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
    public void IfNumberOfCompetitorsIsPowerOf2ShouldDistributeThemInMatches(int numberOfCompetitors)
    {
        var listOfCompetitors = GetListOfCompetitors(numberOfCompetitors);

        var stage = new SingleEliminationStage();
        var isOrderedBySeed = true;

        stage.CreateMatches(listOfCompetitors, isOrderedBySeed);
    }

    private List<Competitor> GetListOfCompetitors(int numberOfCompetitors)
    {
        var listOfCompetitors = new List<Competitor>();
        for (int i = 0; i < numberOfCompetitors; i++)
        {
            listOfCompetitors.Add(new Competitor($"Competitor-{i}", i));
        }

        return listOfCompetitors;
    }
}
