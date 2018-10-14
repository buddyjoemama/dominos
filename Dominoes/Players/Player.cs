using System;
using System.Collections.Generic;
using Dominoes.Players.Strategy;

namespace Dominoes.Players
{
    public class Player
    {
        private IStrategy _strategy;
        private DominoList _list = null;
        private List<Domino> _myDominoes = new List<Domino>();
        private String _name = null;
        private Train _train = null; // Players private train.

        public Player(String name, DominoList list, IStrategy strategy)
        {
            _name = name;
            _list = list;
            _strategy = strategy;
            _train = new Train(true);
        }

        public int DominosOnHand => _myDominoes.Count;
        public bool IsTrainPrivate => _train.IsPrivate;
        public bool Won => _myDominoes.Count == 0;
        public String Name => _name;

        /// <summary>
        /// Use the primary strategy first then fallback to default.
        /// If default strategy cant be played, just pick.
        /// </summary>
        public void Play()
        {
            if (_strategy.CanPlay(_myDominoes, _train))
                _strategy.Play(_myDominoes, _train);
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
        }

        public void Pick()
        {
            _myDominoes.Add(_list.TakeNextAvailable());
        }

        public String PrintTrain()
        {
            return _train.ToString();
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
