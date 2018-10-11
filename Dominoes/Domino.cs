using System;
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
    }
}
