﻿using Dominoes.Players;
using Dominoes.Players.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominoes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating Dominoes");

            List<String> winners = new List<string>();

            for (int i = 1; i < 100000; i++)
            {
                Console.WriteLine("Playing game: " + i);

                try
                {
                    Player player1 = new Player("player1", new EricsStrategy());
                    Player player2 = new Player("player2", new TopDownPublicAnyStrategy());

                    GameManager manager = GameManager.Init(player1, player2);
                    manager.StartGame();

                    while (true)
                    {
                        foreach (Player player in GameManager.Instance.Players)
                        {
                            player.Play();
                        }
                    }
                }
                catch (PlayerLostException e)
                {
                    //Console.WriteLine("Player: " + e.Player.Name + " lost.");
                    var winner = GameManager.Instance.Players.Single(s => s.Name != e.Player.Name);

                    winners.Add(winner.Name);
                }
                catch (PlayerWonException e)
                {
                   // Console.WriteLine("Player: " + e.Player.Name + " won!");
                    winners.Add(e.Player.Name);
                }
            }

            var g = winners.GroupBy(s => s);
            foreach(var group in g)
            {
                Console.WriteLine(group.Key + ": " + group.Count());
            }
        }
    }

}
