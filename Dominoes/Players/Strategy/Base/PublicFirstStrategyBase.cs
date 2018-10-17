using System;
using System.Collections.Generic;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public abstract class PublicFirstStrategyBase : OrderedStrategyBase
    {
        private Train _trainToPlay = null;

        public override (bool canPlay, List<Domino> nextToPlay) CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            var canPlay = base.CanPlay(myDominoes, GameManager.Instance.PublicTrain);

            if (canPlay.canPlay)
            {
                _trainToPlay = GameManager.Instance.PublicTrain;
                return canPlay;
            }

            _trainToPlay = myTrain;
            return base.CanPlay(myDominoes, myTrain);
        }
    }
}
