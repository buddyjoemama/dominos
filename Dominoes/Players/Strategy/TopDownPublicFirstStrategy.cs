using System;
using System.Collections.Generic;
using System.Linq;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    /// <summary>
    /// Like top down, but preferentially use the public train.
    /// </summary>
    public class TopDownPublicFirstStrategy : PublicFirstOrderedStrategyBase
    {   
        protected override Func<IEnumerable<Domino>, IOrderedEnumerable<Domino>> 
            OrderingFunction => (s) => s.OrderByDescending(t => t.Sum);
    }
}
