using System;
using System.Collections.Generic;

namespace Dominoes
{
    public class DominoList
    {
        public DominoList()
        {
            Generate();     
        }

        private Queue<Domino> _dominoes = null;

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

                    if (dominoe.IsDouble && FirstDouble == null)
                        FirstDouble = dominoe;
                    else
                        _dominoes.Enqueue(dominoe);

                    rawList.Remove(index);
                }
            }

            MaxDominos = _dominoes.Count;
        }

        public Domino FirstDouble { get; private set;  }

        public int MaxDominos { get; private set; }
        public bool IsEmpty => _dominoes.Count == 0;

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
