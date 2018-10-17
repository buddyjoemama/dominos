using System;
using System.Collections.Generic;
using Dominoes.Players;
using Dominoes.Players.Strategy;
using System.Linq;
using Dominoes.Trains;

namespace Dominoes
{
    public class GameManager
    {
        public static GameManager Instance { get; private set; } = null;
        private PlayerList _playerList = new PlayerList();
        private DominoList _dominoList = null;

        protected GameManager(DominoList dominos, params Player[] players) 
        {
            _playerList.Clear();
            _playerList.AddPlayers(players);
            PublicTrain = new PublicTrain();
            _dominoList = dominos;
        }

        public static void Init(DominoList dominos, params Player[] players)
        {
            Instance = new GameManager(dominos, players);
        }

        /// <summary>
        /// Starts the game...begins by iterating over the players and picking 
        /// their dominos.
        /// </summary>
        public void StartGame()
        {       
            while (_playerList.All(s => s.DominosOnHand < 12))
            {
                foreach (Player player in _playerList)
                {
                    if (player.DominosOnHand < 12)
                        player.Pick();
                }
            }

            foreach(Player player in _playerList)
            {
                Console.WriteLine(player.ToString());
            }
        }

        /// <summary>
        /// Loop over the players and play each. 
        /// One iteration is a complete turn.
        /// </summary>
        public void TakeTurn()
        {
            Console.Clear();

            foreach(Player player in _playerList)
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
        /// Gets the public train.
        /// </summary>
        /// <value>The public train.</value>
        public PublicTrain PublicTrain { get; private set; }

        public bool Draw => _playerList.All(s => !s.CanPlay) && _dominoList.IsEmpty;

        public List<Train> GetAvailablePublicTrains()
        {
            return _playerList.AllAvailableTrains();
        }
    }
}
