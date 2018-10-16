using System;
using System.Collections;
using System.Collections.Generic;
using Dominoes.Trains;
using System.Linq;

namespace Dominoes.Players
{
    public class PlayerList : List<Player>
    {
        public PlayerList()
        {
        }

        /// <summary>
        /// Adds a player to the master player list.
        /// </summary>
        /// <param name="player">Player.</param>
        public void AddPlayer(Player player)
        {
            this.Add(player);
        }

        internal void AddPlayers(Player[] players)
        {
            this.AddRange(players);
        }

        internal List<Train> AllAvailableTrains()
        {
            return this.Where(s => !s.IsTrainPrivate)
                       .Select(s => s.Train)
                       .ToList();
        }
    }
}
