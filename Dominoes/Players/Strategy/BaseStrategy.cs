using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominoes.Players.Strategy
{
    public abstract class BaseStrategy : IStrategy
    {
        public virtual bool CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            return myDominoes.Any(s => myTrain.CanPlayDomino(s));
        }

        public virtual void Play(List<Domino> myDominoes, Train myTrain) {}
    }

    /// <summary>
    /// Top-down strategy...pick the highest point first.
    /// </summary>
    public class TopDownStrategy : BaseStrategy
    {
        public override void Play(List<Domino> myDominoes, Train myTrain)
        {
            Domino domino = null;
            int skip = 0;
            do
            {
                domino = myDominoes
                    .OrderByDescending(s => s.Sum)
                    .Skip(skip)
                    .Take(1)
                    .First();

                skip += 1;
            } while (!myTrain.CanPlayDomino(domino));

            myTrain.PlayDomino(domino);
        }
    }

    public class BottomUpStrategy : BaseStrategy
    {

    }

    public class PublicFirstStrategy : BaseStrategy
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
    public class EricsStrategy : PublicFirstStrategy
    {

    }
}
