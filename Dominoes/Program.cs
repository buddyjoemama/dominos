using Dominoes.Players;
using Dominoes.Players.Strategy;
using System;
using System.Collections.Generic;

namespace Dominoes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating Dominoes");

            try
            {
                Player player1 = new Player("player1", new EricsStrategy());
                Player player2 = new Player("player2", new TopDownPublicAnyStrategy());

                GameManager manager = GameManager.Init(player1, player2);
                manager.StartGame();

                while(true)
                {
                    manager.TakeTurn();
                }
            }
            catch(PlayerWonException e)
            {
                Console.WriteLine("Player: " + e.Player.Name + " won!");
            }
        }
    }

}
