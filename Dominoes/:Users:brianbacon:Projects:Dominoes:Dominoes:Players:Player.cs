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

        public Player(DominoList list, IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void Play()
        {
            _strategy.Play();
        }

        public void Pick()
        {
            _myDominoes.Add(_list.TakeNextAvailable());
        }
    }
}
