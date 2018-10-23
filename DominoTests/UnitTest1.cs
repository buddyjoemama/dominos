using Dominoes;
using Dominoes.Players;
using Dominoes.Players.Strategy;
using Dominoes.Trains;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tree;

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
            playerTwo.Play();
            playerTwo.Play();
            playerTwo.Play();
            playerTwo.Play();
        }
    }    
}
