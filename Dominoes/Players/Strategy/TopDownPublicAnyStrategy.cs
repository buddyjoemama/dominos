using System;
using System.Collections.Generic;
using System.Linq;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public class TopDownPublicAnyStrategy : TopDownPublicFirstStrategy
    {
        public TopDownPublicAnyStrategy()
        {
        }

        public override (bool canPlay, List<Domino> nextToPlay, Train trainToPlay) 
            CanPlay(List<Domino> myDominoes, Train myTrain, Player player)
        {
            if (base.CanPlay(myDominoes, GameManager.Instance.PublicTrain, player).canPlay)
                return base.CanPlay(myDominoes, GameManager.Instance.PublicTrain, player);

            // Can we play on any other players public train?
            foreach(var train in GameManager.Instance.GetAvailablePublicTrains())
            {
                // Pick the first one to play on
                var canPlay = base.CanPlay(myDominoes, train, player);
                if(canPlay.canPlay)
                {
                    return canPlay;
                }
            }

            // Else play on my train.
            return base.CanPlay(myDominoes, myTrain, player);
        }
    }
}
