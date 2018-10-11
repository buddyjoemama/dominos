using System;
using System.Collections.Generic;
using Dominoes.Players;
using Dominoes.Players.Strategy;
using System.Linq;

namespace Dominoes
{
    public static class GameManager
    {
        private static DominoList dominoList = new DominoList();
        private static List<Player> _players = new List<Player>();
        public static int DOMINO_START_COUNT = 12;

        public static void Begin()
        {
            dominoList.Generate();

            Console.WriteLine("Head of private trains: " + dominoList.Master.ToString());

            _players.Clear();
            _players.Add(new Player("One", dominoList, new TopDownStrategy()));
            //_players.Add(new Player("Two", dominoList, new BottomUpStrategy()));
            //_players.Add(new Player("Three", dominoList, new PublicFirstStrategy()));
            //_players.Add(new Player("Four", dominoList, new PrivateFirstStrategy()));
            //_players.Add(new Player("Eric", dominoList, new EricsStrategy()));

            while (_players.All(s => s.DominosOnHand < DOMINO_START_COUNT))
            {
                foreach (Player player in _players)
                {
                    if (player.DominosOnHand < DOMINO_START_COUNT)
                        player.Pick();
                }
            }

            foreach(Player player in _players)
            {
                Console.WriteLine(player.ToString());
            }
        }

        public static void TakeTurn()
        {
            foreach(Player player in _players)
            {
                if (!player.Won)
                    player.Play();
                else
                    throw new PlayerWonException(player);
            }
        }
    }
}
