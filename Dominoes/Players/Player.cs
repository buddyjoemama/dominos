using System;
using System.Collections.Generic;
using Dominoes.Players.Strategy;
using Dominoes.Trains;
using System.Linq;

namespace Dominoes.Players
{
    public class Player
    {
        private IStrategy _strategy;
        private DominoList _list = null;
        private List<Domino> _myDominoes = new List<Domino>();
        private Train _train = null; // Players private train.

        public Player(String name, DominoList list, IStrategy strategy)
        {
            Name = name;
            _list = list;
            _strategy = strategy;
            _train = new Train(true, list.FirstDouble);
        }

        /// <summary>
        /// Take n number of dominos
        /// </summary>
        /// <param name="v">V.</param>
        public void Take(int v)
        {
            for (int i = 0; i < v; i++)
                Pick();
        }

        public int DominosOnHand => _myDominoes.Count;
        public int Turns { get; private set; }
        public bool IsTrainPrivate => _train.IsPrivate;
        public bool Won => _myDominoes.Count == 0;
        public String Name { get; private set; }
        public Train Train => _train;

        public bool HasDouble => _myDominoes.Any(s => s.IsDouble);

        /// <summary>
        /// Use the primary strategy first then fallback to default.
        /// If default strategy cant be played, just pick.
        /// </summary>
        public void Play()
        {
            (bool canPlay, List<Domino> playList, Train trainToPlay) = _strategy.CanPlay(_myDominoes, _train);

            if (canPlay)
            {
                _strategy.Play(playList, _myDominoes, trainToPlay);
            }
            else
            {
                try
                {
                    Pick();
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine("Pick list is empty");
                }
            }

            Turns += 1;
        }

        public void Pick()
        {
            _myDominoes.Add(_list.TakeNextAvailable());
        }

        public String PrintTrain()
        {
            return _train.ToString();
        }

        public bool CanPlay
        {
            get
            {
                var val = _strategy.CanPlay(_myDominoes, Train);

                if (!val.canPlay)
                    Train.MakePublic();
                else
                    Train.MakePrivate();

                return val.canPlay;
            }
        }

        public override string ToString()
        {
            String list = "";
            foreach(Domino d in _myDominoes)
            {
                list += d.ToString() + " ";
            }

            return list;
        }
    }
}
