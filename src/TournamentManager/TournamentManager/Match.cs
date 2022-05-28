using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public class Match
{
    private const int maxCompetitorsAllowed = 2;
    private List<Competitor> competitors;

    public IReadOnlyCollection<Competitor> Competitors { get => new ReadOnlyCollection<Competitor>(competitors); }
    public Competitor? Winner { get; set; }
    public Competitor? Loser { get; set; }
    public int MatchNumber { get; set; }
    public int RoundNumber { get; set; }

    public Match()
    {
        competitors = new List<Competitor>();
    }

    public Match(IEnumerable<Competitor> listOfCompetitors)
    {
        competitors = new List<Competitor>();
        AddCompetitor(listOfCompetitors);
    }

    public void AddCompetitor(Competitor competitor)
    {
        if (!IsAvailableToAddCompetitor())
        {
            throw new InvalidOperationException($"Não é possível adicionar mais de {maxCompetitorsAllowed} competidores.");
        }

        competitors.Add(competitor);
    }

    public void AddCompetitor(IEnumerable<Competitor> listOfCompetitors)
    {
        if (!IsAvailableToAddCompetitor(listOfCompetitors))
        {
            throw new InvalidOperationException($"Não é possível adicionar mais de {maxCompetitorsAllowed} competidores.");
        }

        competitors.AddRange(listOfCompetitors);
    }

    public void SetWinner(Competitor winner)
    {
        if (competitors.Count == 0)
        {
            throw new InvalidOperationException("Não é possível terminar uma partida sem competidores.");
        }
        if (!competitors.Contains(winner))
        {
            throw new InvalidOperationException($"Este competidor não está participando da partida.");
        }

        Winner = winner;
        Loser = competitors.FirstOrDefault(c => c != winner);
    }

    public bool IsAvailableToAddCompetitor() => Competitors.Count < maxCompetitorsAllowed;
    public bool IsAvailableToAddCompetitor(IEnumerable<Competitor> competitors) => competitors.Count() <= maxCompetitorsAllowed;
}
