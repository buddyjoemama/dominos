﻿using System.Collections.Generic;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    /// <summary>
    /// Build private train, dont use private unless need to.
    /// Preferentially use public.
    /// </summary>
    public class EricsStrategy : TopDownPublicFirstStrategy
    {
        public override (bool canPlay, List<Domino> nextToPlay, Train trainToPlay) 
            CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            return base.CanPlay(myDominoes, myTrain);
        }
    }
}
