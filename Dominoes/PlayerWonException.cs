using System;
using System.Runtime.Serialization;
using Dominoes.Players;

namespace Dominoes
{
    [Serializable]
    internal class PlayerWonException : Exception
    {
        public Player player;

        public PlayerWonException()
        {
        }

        public PlayerWonException(Player player)
        {
            this.player = player;
        }

        public PlayerWonException(string message) : base(message)
        {
        }

        public PlayerWonException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlayerWonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}