using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class Competitor
{
    public string Name { get; set; }
    public int Seed { get; set; }

    public Competitor(string name, int seed)
    {
        Name = name;
        Seed = seed;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Competitor) 
            return false;

        var competitor = obj as Competitor;

        return this.Name == competitor.Name;
    }
}
