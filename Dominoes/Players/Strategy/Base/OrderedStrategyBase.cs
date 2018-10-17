using System;
using System.Collections.Generic;
using System.Linq;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public abstract class OrderedStrategyBase : StrategyBase
    {
        public override (bool canPlay, List<Domino> nextToPlay, Train trainToPlay) CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            var domino = OrderingFunction(myDominoes)
                       .FirstOrDefault(s => myTrain.CanPlayDomino(s));

            if (domino == null)
            {
                return (false, null, null);
            }

            if (domino.IsDouble && FindDoubleAndFollowUp(myDominoes) != null)
            {
                var dominos = FindDoubleAndFollowUp(myDominoes);
                return (true, dominos, myTrain);
            }
            else if (domino.IsDouble)
            {
                // We dont have a follow up...so we need to try a different domino
                List<Domino> l = new List<Domino>(myDominoes);
                l.Remove(domino);

                return CanPlay(l, myTrain);
            }

            return (true, new List<Domino> { domino }, myTrain);
        }

        protected override List<Domino> FindDoubleAndFollowUp(List<Domino> myDominoes)
        {
            // We may not have any doubles...
            if (myDominoes.All(s => !s.IsDouble))
                return null;

            var firstDouble = OrderingFunction(myDominoes)
                                        .First(s => s.IsDouble);

            // Find the follow up
            var followUp = OrderingFunction(myDominoes)
                                     .FirstOrDefault(s => !s.IsDouble && s.CanAttachAny(firstDouble));

            if (followUp == null)
                return null;

            return new List<Domino> { firstDouble, followUp };
        }

        protected abstract Func<IEnumerable<Domino>, IOrderedEnumerable<Domino>> OrderingFunction { get; }
    }
}
