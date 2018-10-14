using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominoes.Players.Strategy
{
    public abstract class BaseOrderedStrategy : BaseStrategy
    {
        public override bool CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            if (GameManager.MasterPrivateDomino == null)
            {
                var dominos = FindDoubleAndFollowUp(myDominoes);
                if (dominos == null)
                    return false;

                _nextToPlay.Add(dominos.Item1);
                _nextToPlay.Add(dominos.Item2);

                return true;
            }
            else
            {
                var domino = OrderingFunction(myDominoes)
                                       .FirstOrDefault(s => myTrain.CanPlayDomino(s));

                if (domino == null)
                    return false;

                if (domino.IsDouble && FindDoubleAndFollowUp(myDominoes) != null)
                {
                    var dominos = FindDoubleAndFollowUp(myDominoes);

                    _nextToPlay.Add(dominos.Item1);
                    _nextToPlay.Add(dominos.Item2);
                    return true;
                }
                else if (domino.IsDouble)
                {
                    // We dont have a follow up...so we need to try a different domino
                    List<Domino> l = new List<Domino>(myDominoes);
                    l.Remove(domino);

                    return CanPlay(l, myTrain);
                }

                _nextToPlay.Add(domino);
                return true;
            }
        }

        protected override Tuple<Domino, Domino> FindDoubleAndFollowUp(List<Domino> myDominoes)
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

            return new Tuple<Domino, Domino>(firstDouble, followUp);
        }

        protected abstract Func<IEnumerable<Domino>, IOrderedEnumerable<Domino>> OrderingFunction { get; }
    }
}
