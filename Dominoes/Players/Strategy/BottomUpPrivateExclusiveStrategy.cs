using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominoes.Players.Strategy
{
    /// <summary>
    /// Pick the lowest first...only play on private train.
    /// </summary>
    public class BottomUpPrivateExclusiveStrategy : OrderedStrategyBase
    {
        protected override Func<IEnumerable<Domino>, IOrderedEnumerable<Domino>> OrderingFunction =>
            (s) => s.OrderBy(t => t.Sum);
    }
}
