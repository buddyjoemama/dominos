using System;
using System.IO;
using Dominoes;
using Dominoes.Players;
using Dominoes.Players.Strategy;
using Dominoes.Trains;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Tree;
using System.Linq;

namespace DominoTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestPlayerHasInitialTrain()
        {
            DominoList dominos = new DominoList();

            Player playerOne = new Player("Player One", dominos, new TopDownPrivateExclusiveStrategy());
            playerOne.Take(12);

            GameManager.Init(dominos, playerOne);

            // Players first domino is the first double picked from the list.
            Assert.IsTrue(!playerOne.Train.IsEmpty);

            playerOne.Play();

            // This player does not play on the public train
            Assert.IsTrue(GameManager.Instance.PublicTrain.IsEmpty);
        }

        [TestMethod]
        public void TestPlayerCanPlayOnPublicTrain()
        {
            DominoList dominos = new DominoList();

            Player playerOne = new Player("Player One", dominos, new TopDownPublicFirstStrategy());
            playerOne.Take(12);

            // Need to first play a double on the public train.
            // Make sure we have one.
            while(!playerOne.HasDouble)
            {
                playerOne.Pick();
            }

            Assert.IsTrue(playerOne.HasDouble);
            GameManager.Init(dominos, playerOne);

            // Should play on the public train first.
            playerOne.Play();

            Assert.IsFalse(GameManager.Instance.PublicTrain.IsEmpty);
        }

        [TestMethod]
        public void TestPlayerCanPlayOnOtherPlayersPublicTrain()
        {
            DominoList dominos = new DominoList();

            Player playerOne = new Player("Player One", dominos, new TopDownPublicAnyStrategy());
            playerOne.Take(12);

            Player playerTwo = new Player("Player Two", dominos, new TopDownPrivateExclusiveStrategy());
            playerTwo.Take(12);

            GameManager.Init(dominos, playerOne, playerTwo);

            // Play player 2 until he cant play anymore...should be public train.
            while (playerTwo.CanPlay)
            {
                playerTwo.Play();
            }

            Assert.IsFalse(playerTwo.Train.IsPrivate);

            playerOne.Play();

            try
            {
                while (!playerOne.Won)
                {
                    playerOne.Play();
                }
            }
            catch(PlayerLostException)
            {
                // This player can no longer play...
            }
            catch(PlayerWonException) {}
        }

        [TestMethod]
        public void TestEricsStrategy()
        {
            DominoList dominos = new DominoList();

            Player playerOne = new Player("Brian", dominos, new TopDownPublicAnyStrategy());
            playerOne.Take(12);

            Player playerTwo = new Player("Eric", dominos, new EricsStrategy());
            playerTwo.Take(12);

            GameManager.Init(dominos, playerOne, playerTwo);

            playerOne.Play();
            playerTwo.Play();

            try
            {
                while(!playerTwo.Won)
                {
                    playerTwo.Play();
                }
            }
            catch(Exception e)
            {

            }
        }

        public static TestContext _context = null;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _context = context;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var atts = this.GetType().GetMethod(_context.TestName)
                           .GetCustomAttributes(typeof(TestPropertyAttribute), true)
                           .Select(s => s as TestPropertyAttribute)
                           .ToList();
                           
            if (atts != null && atts.Count > 0)
            {
                String fileName = atts.FirstOrDefault(s=>s.Name == "game")?.Value;

                if (fileName != null && File.Exists(fileName))
                {
                    var game = JsonConvert.DeserializeObject<GameConfiguration>(File.ReadAllText(fileName));

                    if (_context.Properties.ContainsKey("game"))
                        _context.Properties.Remove("game");

                    var newGame = GameManager.CreateFromConfig(game);

                    if (newGame != null)
                    {
                        _context.Properties.Add("game", newGame);
                    }
                }
            }
        }

        [TestMethod]
        [TestCategory("gamelogic")]
        [TestProperty("game", "TestDominoCreation.json")]
        [ExpectedException(typeof(PlayerWonException))]
        public void TestDominoCreation()
        {
            GameManager manager = _context.Properties["game"] as GameManager;

            var player1 = manager.GetPlayer("player1");
            player1.Play();
            player1.Play();
        }

        [TestMethod]
        [TestProperty("game", "TestEricsStrategyPublicOnly.json")]
        [ExpectedException(typeof(PlayerWonException))]
        public void TestEricsStrategyPublicOnly()
        {
            GameManager manager = _context.Properties["game"] as GameManager;
            Assert.IsNotNull(manager);

            var player1 = manager.GetPlayer("player1");

            try
            {
                player1.Play();
            }
            catch(PlayerWonException)
            {
                Assert.IsTrue(manager.PublicTrain.Dominos == 2);
                Assert.IsTrue(player1.Train.Dominos == 1); // The first double selected to be the root of all private trains.
                throw;
            }
        }
    }    
}
