using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager.Tests;
public static class HelperTests
{
    public static List<Competitor> GetListOfCompetitors(int numberOfCompetitors)
    {
        var listOfCompetitors = new List<Competitor>();
        for (int i = 1; i <= numberOfCompetitors; i++)
        {
            listOfCompetitors.Add(new Competitor($"Competitor-{i}", i));
        }

        return listOfCompetitors;
    }
}
