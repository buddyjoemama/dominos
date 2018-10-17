using System;
using System.Collections.Generic;
using System.Linq;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    /// <summary>
    /// Top-down strategy...pick the highest point first.
    /// </summary>
    public class TopDownPrivateExclusiveStrategy : BaseOrderedStrategy
    {
        protected override Func<IEnumerable<Domino>, IOrderedEnumerable<Domino>> OrderingFunction =>
            (s) => s.OrderByDescending(t => t.Sum);
    }
}
