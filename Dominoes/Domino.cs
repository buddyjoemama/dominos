using System;
using System.Text.RegularExpressions;

namespace Dominoes
{
    public class Domino
    {
        public Domino(String name, int lVal, int rVal)
        {
            Name = name;
            LeftValue = lVal;
            RightValue = rVal;
        }

        public Domino(string domino)
        {
            Name = domino;

            var group = Regex.Match(domino, @"\[(?<left>\d+),(?<right>\d+)\]");
            LeftValue = Convert.ToInt16(group.Groups["left"].Value);
            RightValue = Convert.ToInt16(group.Groups["right"].Value);
        }

        public String Name { get; set; }
        public int LeftValue { get; set; }
        public int RightValue { get; set; }
        public bool IsDouble => LeftValue == RightValue;

        public override string ToString()
        {
            return $"[{LeftValue}|{RightValue}]";
        }

        public void Flip()
        {
            var l = LeftValue;
            var r = RightValue;

            RightValue = l;
            LeftValue = r;
        }

        public int Sum => LeftValue + RightValue;

        public bool MarkForPrivatePlay { get; internal set; }

        /// <summary>
        /// Can this domino be attached to either side of the double.
        /// </summary>
        internal bool CanAttachAny(Domino doubleDomino)
        {
            return this.CanAttachLeft(doubleDomino) 
            || this.CanAttachRight(doubleDomino);
        }

        internal bool CanAttachLeft(Domino domino)
        {
            return this.LeftValue == domino.LeftValue ||
                       this.LeftValue == domino.RightValue;
        }

        internal bool CanAttachRight(Domino domino)
        {
            return this.RightValue == domino.RightValue ||
                       this.RightValue == domino.LeftValue;
        }
    }
}
