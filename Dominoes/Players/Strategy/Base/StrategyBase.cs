using System;
using System.Collections.Generic;
using System.Linq;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public abstract class StrategyBase : IStrategy
    {
        public virtual (bool canPlay, List<Domino> nextToPlay, Train trainToPlay) 
            CanPlay(List<Domino> myDominoes, Train myTrain, Player player)
        {
            return (myDominoes.Any(s => myTrain.CanPlayDomino(s)), null, myTrain);
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
            // We may not have any doubles...
            if (myDominoes.All(s => !s.IsDouble))
                return null;

            var firstDouble = myDominoes.First(s => s.IsDouble);

            // Find the follow up
            var followUp = myDominoes.FirstOrDefault(s => !s.IsDouble && s.CanAttachAny(firstDouble));

            if (followUp == null)
                return null;

            return new List<Domino> { firstDouble, followUp };
        }

        public virtual void Pick(List<Domino> playersDominos, DominoList sourceList)
        {
            playersDominos.Add(sourceList.TakeNextAvailable());
        }

        public virtual void Initialize(Player player, Train train, List<Domino> playerDominos) {}
    }
}
