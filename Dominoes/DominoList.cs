using System;
using System.Collections.Generic;

namespace Dominoes
{
    public class DominoList
    {
        private Queue<Domino> _dominoes = null;
        public Domino Master { get; set; }

        /// <summary>
        /// Clear the list of dominos and regenerate the pick queue
        /// </summary>
        public void Generate()
        {
            _dominoes = new Queue<Domino>();
            Dictionary<int, Domino> rawList = new Dictionary<int, Domino>();

            int count = 0;
            for (int i = 0; i <= 12; i++)
            {
                for (int x = i; x <= 12; x++)
                {
                    rawList.Add((count++), new Domino($"[{i},{x}]", i, x));
                }
            }

            // Store in a queue...load randomly 
            Random rand = new Random((int)DateTime.UtcNow.Ticks);
            while (rawList.Count > 0)
            {
                int index = rand.Next(0, 91);
                if (rawList.ContainsKey(index))
                {
                    Domino dominoe = rawList[index];

                    if(dominoe.IsDouble && Master == null)
                    {
                        Master = dominoe;
                    }
                    else
                        _dominoes.Enqueue(dominoe);

                    rawList.Remove(index);
                }
            }
        }

        /// <summary>
        /// Gets the next dominoe from the queue.
        /// </summary>
        /// <returns>The next available.</returns>
        public Domino TakeNextAvailable()
        {
            return _dominoes.Dequeue();
        }
    }
}
