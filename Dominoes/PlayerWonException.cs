using System;
using System.Runtime.Serialization;
using Dominoes.Players;

namespace Dominoes
{
    [Serializable]
    internal class PlayerWonException : Exception
    {
        public Player Player { get; private set; }

        public PlayerWonException(Player player)
        {
            this.Player = player;
        }
    }
}