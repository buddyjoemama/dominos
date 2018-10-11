using System;
using System.Collections.Generic;
using System.Linq;

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

    /// <summary>
    /// Top-down strategy...pick the highest point first.
    /// </summary>
    public class TopDownPrivateExclusiveStrategy : BaseStrategy
    {   
        public override bool CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            if (GameManager.MasterPrivateDomino == null)
            {
                var dominos = FindDoubleAndFollowUp(myDominoes);
                if(dominos == null)                
                    return false;

                _nextToPlay.Add(dominos.Item1);
                _nextToPlay.Add(dominos.Item2);

                return true;
            }
            else
            {
                var domino = myDominoes.OrderByDescending(s => s.Sum)
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

            var firstDouble = myDominoes.OrderByDescending(s => s.Sum)
                                        .First(s => s.IsDouble);

            // Find the follow up
            var followUp = myDominoes.OrderByDescending(s => s.Sum)
                                     .FirstOrDefault(s => !s.IsDouble && s.CanAttachAny(firstDouble));

            if (followUp == null)
                return null;

            return new Tuple<Domino, Domino>(firstDouble, followUp);
        }
    }

    public class BottomUpPrivateExclusiveStrategy : BaseStrategy
    {
        public override void Play(List<Domino> myDominoes, Train myTrain)
        {

        }
    }

    public class TopDownPublicFirstStrategy : TopDownPrivateExclusiveStrategy
    {

    }

    public class PrivateFirstStrategy : BaseStrategy
    {

    }

    public class DefaultStrategy : BaseStrategy
    {

    }

    /// <summary>
    /// Build private train, dont use private unless need to.
    /// Preferentially use public.
    /// </summary>
    public class EricsStrategy : TopDownPublicFirstStrategy
    {

    }
}
