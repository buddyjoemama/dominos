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

            _players.Clear();
            _players.Add(new Player("One", dominoList, new TopDownPrivateExclusiveStrategy()));
            //_players.Add(new Player("Two", dominoList, new BottomUpStrategy()));
            //_players.Add(new Player("Three", dominoList, new PublicFirstStrategy()));
            //_players.Add(new Player("Four", dominoList, new PrivateFirstStrategy()));
            //_players.Add(new Player("Eric", dominoList, new EricsStrategy()));

            PublicTrain = new GlobalPublicTrain();

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

        /// <summary>
        /// Loop over the players and play each. 
        /// One iteration is a complete turn.
        /// </summary>
        public static void TakeTurn()
        {
            Console.Clear();

            int cursorPosition = Console.CursorTop;

            foreach(Player player in _players)
            {
                if (!player.Won)
                    player.Play();
                else
                    throw new PlayerWonException(player);

                Console.WriteLine("Player: " + player.Name);
                Console.WriteLine("Dominoes: " + player.ToString());
                Console.WriteLine("\nTrain: " + player.PrintTrain());
            }

            Console.WriteLine("\n\nEnter to continue...");

            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }

        /// <summary>
        /// Master public train.
        /// </summary>
        /// <value>The public train.</value>
        public static Train PublicTrain { get; set; }

        /// <summary>
        /// List of readonly players
        /// </summary>
        /// <value>The players.</value>
        public static IReadOnlyList<Player> Players => _players.AsReadOnly();

        public static Domino MasterPrivateDomino { get; set; }

        public static Domino MasterPublicDomino { get; set; }
    }
}
