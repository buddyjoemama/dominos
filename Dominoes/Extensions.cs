using System;
using System.Linq;
using System.Collections.Generic;
using Dominoes.Trains;

namespace Dominoes
{
    public static class Extensions
    {   
        public static IEnumerable<Domino> Without(this IEnumerable<Domino> list, 
                                                  Domino domino)
        {
            return list.Except(new List<Domino> { domino });
        }

        public static List<Domino> Minus(this IEnumerable<Domino> list, Train train)
        {
            return list.Except(train.Dump()).ToList();
        }

        public static IEnumerable<Domino> Each(this IEnumerable<Domino> list, 
                                               Func<Domino, Domino> exp)
        {
            foreach(var d in list)
            {
                exp(d);
                yield return d;
            }
        }

        public static List<Domino> ToList(this Domino domino)
        {
            return new List<Domino> { domino };
        }

        public static Domino ToDomino(this string domino)
        {
            return new Domino(domino);
        }

        public static List<Domino> ToDominos(this String[] dominos)
        {
            return dominos?.Select(s => new Domino(s)).ToList();
        }
    }
}
