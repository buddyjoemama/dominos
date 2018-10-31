using System;
using System.Collections.Generic;
using Dominoes.Players;
using System.Linq;

namespace Dominoes.Trains
{
    public class Train
    {
        protected LinkedList<Domino> _train = new LinkedList<Domino>();
        private Player _player = null;

        /// <summary>
        /// First double is the head of the private train.
        /// </summary>
        /// <param name="isPrivate">If set to <c>true</c> is private.</param>
        /// <param name="firstDouble">First double.</param>
        public Train(bool isPrivate, Domino firstDouble, Players.Player player)
        {
            IsPrivate = isPrivate;
            _player = player;

            if(firstDouble != null)
                _train.AddFirst(firstDouble);
        }

        /// <summary>
        /// THe domino can be played if the right or left match the last domino
        /// </summary>
        public virtual bool CanPlayDomino(Domino domino)
        {
            return _train.Last.Value.CanAttachRight(domino);
        }

        /// <summary>
        /// At this point we've determined the domino can be 
        /// played on this train.
        /// </summary>
        /// <param name="domino">Domino.</param>
        public virtual void PlayDomino(Domino domino)
        {
            // Else we have to add it to the right side
            if (_train.Last.Value.RightValue == domino.LeftValue ||
               _train.Last.Value.RightValue == domino.RightValue)
            {
                if (_train.Last.Value.RightValue != domino.LeftValue)
                    domino.Flip();

                _train.AddLast(domino);
            }
        }

        public void MakePublic()
        {
            IsPrivate = false;
        }

        public virtual void MakePrivate()
        {
            IsPrivate = true;
        }

        internal Domino Pop()
        {
            var first = _train.First();
            _train.RemoveFirst();

            return first;
        }

        public override string ToString()
        {
            String list = "";
            foreach (Domino d in _train)
            {
                list += d.ToString() + " ";
            }

            return list;
        }

        public bool IsPrivate { get; private set; }

        public bool IsEmpty => _train.Count == 0;

        public Domino FirstDomino => _train?.First?.Value;

        public int Dominos => _train.Count;

        public int TotalDots => _train.Sum(t => t.Sum);

        public List<Domino> Dump()
        {
            return _train.ToList();
        }

        public virtual bool IsMyTrain(Player player)
        {
            return _player == player;
        }
    }
}
