using System;
using Xunit;

namespace TournamentManager.Tests;
public class MatchTests
{
    [Fact]
    public void WhenItsGivenAWinnerItShouldSetBothWinnerAndLoser()
    {
        //Arranje
        var match = new Match();
        var competitor1 = new Competitor("Lombardi", 1);
        var competitor2 = new Competitor("Silvio", 2);

        match.AddCompetitor(competitor1);
        match.AddCompetitor(competitor2);

        //Act
        match.SetWinner(competitor1);

        //Assert
        Assert.Equal(competitor1, match.Winner);
        Assert.Equal(competitor2, match.Loser);
    }

    [Fact]
    public void ShouldNotAcceptMoreThanTwoCompetitors()
    {
        var match = new Match();
        var competitor1 = new Competitor("Lombardi", 1);
        var competitor2 = new Competitor("Silvio", 2);
        var competitor3 = new Competitor("Ratinho", 3);

        match.AddCompetitor(competitor1);
        match.AddCompetitor(competitor2);

        Assert.Throws<InvalidOperationException>(() => match.AddCompetitor(competitor3));
    }
}