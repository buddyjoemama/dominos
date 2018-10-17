using Dominoes;
using Dominoes.Players;
using Dominoes.Players.Strategy;
using Dominoes.Trains;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }    
}
