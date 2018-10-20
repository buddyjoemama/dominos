using System;
using System.Runtime.Serialization;

namespace Dominoes.Players
{
    [Serializable]
    public class PlayerLostException : Exception
    {
        public PlayerLostException(Player player)
        {
            this.Player = player;
        }

        public Player Player { get; private set; }
    }
}