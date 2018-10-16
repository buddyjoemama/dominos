using System;
using System.Collections.Generic;
using System.Linq;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public abstract class BaseStrategy : IStrategy
    {
        protected List<Domino> _nextToPlay = new List<Domino>();

        public virtual bool CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            return myDominoes.Any(s => myTrain.CanPlayDomino(s));
        }

        /// <summary>
        /// Plays whatever dominos are queued up.
        /// </summary>
        public virtual void Play(List<Domino> myDominoes, Train myTrain)
        {
            foreach (var d in _nextToPlay)
            {
                myDominoes.Remove(d);
                myTrain.PlayDomino(d);  
            }

            _nextToPlay.Clear();
        }

        protected virtual Tuple<Domino, Domino> FindDoubleAndFollowUp(List<Domino> myDominoes) 
        {
            return null;
        }
    }
}
