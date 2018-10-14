using System.Collections.Generic;

namespace Dominoes.Players.Strategy
{
    /// <summary>
    /// Build private train, dont use private unless need to.
    /// Preferentially use public.
    /// </summary>
    public class EricsStrategy : TopDownPublicFirstStrategy
    {
        public override bool CanPlay(List<Domino> myDominoes, Train myTrain)
        {
            return false;
        }
    }
}
