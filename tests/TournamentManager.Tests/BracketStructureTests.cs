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
}
