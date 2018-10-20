using System.Collections.Generic;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    /// <summary>
    /// Build private train, dont use private unless need to.
    /// Preferentially use public.
    /// </summary>
    public class EricsStrategy : TopDownPublicAnyStrategy
    {
        public override (bool canPlay, List<Domino> nextToPlay, Train trainToPlay) 
            CanPlay(List<Domino> myDominoes, Train myTrain, Player player)
        {
            var play = base.CanPlay(myDominoes, myTrain, player);

            return play;
        }
    }
}
