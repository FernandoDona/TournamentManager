﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager;
public interface IStage
{
    public List<Competitor> Competitors { get; }
}