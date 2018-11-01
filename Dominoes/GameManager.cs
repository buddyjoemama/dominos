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

        public static GameManager Init(DominoList dominos, params Player[] players)
        {
            Instance = new GameManager(dominos, players);
            return Instance;
        }

        public static GameManager Init(params Player[] players)
        {
            DominoList list = new DominoList();
            foreach (var player in players)
                player.SetPickList(list);

            Instance = new GameManager(list, players);
            return Instance;
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
                player.Begin();
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

            Console.WriteLine("\nPublic train: " + PublicTrain.ToString());

            foreach (Player player in _playerList.Where(s=>!s.Lost))
            {
                Console.WriteLine("\nPlayer: " + player.Name);
                Console.WriteLine("Private train: " + player.IsTrainPrivate);
                Console.WriteLine("Dominoes: " + player.ToString());
                Console.WriteLine("Train: " + player.PrintTrain());

                try
                {
                    if (!player.Won)
                        player.Play();
                    else
                        throw new PlayerWonException(player);
                }
                catch(PlayerLostException p)
                {
                    // This player can no longer be played.
                    p.Player.Lost = true;
                }        
            }

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("\nPublic train: " + PublicTrain.ToString());
            foreach (Player player in _playerList.Where(s=>!s.Lost))
            {
                Console.WriteLine("\nPlayer: " + player.Name);
                Console.WriteLine("Private train: " + player.IsTrainPrivate);
                Console.WriteLine("Dominoes: " + player.ToString());
                Console.WriteLine("Train: " + player.PrintTrain());
            }

            Console.WriteLine("\n\nEnter to continue...");

            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }

        /// <summary>
        /// Gets the public train.
        /// </summary>
        /// <value>The public train.</value>
        public PublicTrain PublicTrain { get; private set; }

        public bool Draw => _playerList.All(s => s.Lost);

        public List<Train> GetAvailablePublicTrains()
        {
            return _playerList.AllAvailableTrains();
        }

        public Player GetPlayer(String name)
        {
            return _playerList.Single(s => s.Name == name);
        }

        /// <summary>
        /// Creates from config.
        /// </summary>
        /// <returns>The from config.</returns>
        /// <param name="game">Game.</param>
        public static GameManager CreateFromConfig(GameConfiguration game)
        {
            DominoList list = new DominoList(game.InitialDomino.ToDomino(),
                                             game.PickList.ToDominos());

            List<Player> allPlayers = new List<Player>();
            foreach(var p in game.Players)
            {
                IStrategy strategy = Activator.CreateInstance(Type.GetType(p.Type)) as IStrategy;
                Player newPlayer = new Player(p.Name, list, strategy);

                foreach(var d in p.Dominos)
                {
                    newPlayer.Pick(d.ToDomino());
                }

                allPlayers.Add(newPlayer);
            }

            GameManager.Init(list, allPlayers.ToArray());

            return GameManager.Instance;
        }
    }
}
