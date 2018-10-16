using System;
using System.Collections.Generic;

namespace Dominoes.Trains
{
    public class Train
    {
        protected LinkedList<Domino> _train = new LinkedList<Domino>();

        /// <summary>
        /// First double is the head of the private train.
        /// </summary>
        /// <param name="isPrivate">If set to <c>true</c> is private.</param>
        /// <param name="firstDouble">First double.</param>
        public Train(bool isPrivate, Domino firstDouble)
        {
            IsPrivate = isPrivate;

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
    }
}
