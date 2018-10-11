using System;
using System.Collections.Generic;

namespace Dominoes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating Dominoes");

            GameManager.Begin();
            
            try
            {
                while (true)
                {
                    GameManager.TakeTurn();
                }
            }
            catch(PlayerWonException e)
            {
                Console.WriteLine("Player: " + e.player.Name + " won!");
            }
        }
    }

}
