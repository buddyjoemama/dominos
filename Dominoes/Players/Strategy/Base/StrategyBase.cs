using System;
using System.Collections.Generic;
using System.Linq;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public abstract class StrategyBase : IStrategy
    {
        public virtual (bool canPlay, List<Domino> nextToPlay) CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            return (myDominoes.Any(s => myTrain.CanPlayDomino(s)), null);
        }

        /// <summary>
        /// Plays whatever dominos are queued up.
        /// </summary>
        public virtual void Play(List<Domino> playList, List<Domino> playersDominos, Train myTrain)
        {
            foreach (var d in playList)
            {
                playersDominos.Remove(d);
                myTrain.PlayDomino(d);  
            }
        }

        protected virtual List<Domino> FindDoubleAndFollowUp(List<Domino> myDominoes) 
        {
            return null;
        }
    }
}
