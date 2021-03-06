﻿using System;
using System.Collections.Generic;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public abstract class PublicFirstOrderedStrategyBase : OrderedStrategyBase
    {
        public override (bool canPlay, List<Domino> nextToPlay, Train trainToPlay) 
            CanPlay(List<Domino> myDominoes, Train myTrain, Player player)
        {
            var canPlay = base.CanPlay(myDominoes, GameManager.Instance.PublicTrain, player);

            if (canPlay.canPlay)
            {
                return (canPlay.canPlay, canPlay.nextToPlay, GameManager.Instance.PublicTrain);
            }

            return base.CanPlay(myDominoes, myTrain, player);
        }
    }
}
