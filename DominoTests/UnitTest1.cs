using Dominoes;
using Dominoes.Players;
using Dominoes.Players.Strategy;
using Dominoes.Trains;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DominoTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DominoList dominos = new DominoList();

            Player playerOne = new Player("Player One", dominos, new TopDownPrivateExclusiveStrategy());
            playerOne.Take(12);

            GameManager.Init(playerOne);

            playerOne.Play();

            Assert.IsTrue(GameManager.Instance.PublicTrain.IsEmpty);
        }
    }    
}
