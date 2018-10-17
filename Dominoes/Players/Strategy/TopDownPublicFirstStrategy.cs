﻿using System.Collections.Generic;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    /// <summary>
    /// Like top down, but preferentially use the public train.
    /// </summary>
    public class TopDownPublicFirstStrategy : TopDownPrivateExclusiveStrategy
    {
        private Train _trainToPlay = null;

        public override bool CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            if (base.CanPlay(myDominoes, GameManager.Instance.PublicTrain))
            {
                _trainToPlay = GameManager.Instance.PublicTrain;
                return true;
            }

            _trainToPlay = myTrain;
            return base.CanPlay(myDominoes, myTrain);
        }

        public override void Play(List<Domino> myDominoes, Train myTrain)
        {
            base.Play(myDominoes, _trainToPlay);
        }
    }
}
