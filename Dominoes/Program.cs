using System;
using System.Collections.Generic;

namespace Dominoes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating Dominoes");

            //GameManager.Begin();
            
            try
            {
    
            }
            catch(PlayerWonException e)
            {
                Console.WriteLine("Player: " + e.player.Name + " won!");
            }
        }
    }

}
