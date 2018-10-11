using System;
using System.Collections.Generic;

namespace Dominoes
{
    public class Train
    {
        protected LinkedList<Domino> _train = new LinkedList<Domino>();
        private bool _isPrivate = true;

        public Train(bool isPrivate)
        {
            _isPrivate = isPrivate;
        }

        public virtual bool CanPlayDomino(Domino domino)
        {
            if (GameManager.MasterPrivateDomino == null && domino.IsDouble)
                return true;
            else if(GameManager.MasterPrivateDomino == null && !domino.IsDouble)
            {
                return false;
            }
            else if(GameManager.MasterPrivateDomino != null && 
                    _train.Count == 0 &&
                    (domino.LeftValue == GameManager.MasterPrivateDomino.LeftValue ||
                     domino.LeftValue == GameManager.MasterPrivateDomino.RightValue ||
                     domino.RightValue == GameManager.MasterPrivateDomino.LeftValue ||
                     domino.RightValue == GameManager.MasterPrivateDomino.RightValue))
            {
                return true;
            }

            return _train.Last.Value.RightValue == domino.LeftValue ||
                         _train.Last.Value.RightValue == domino.RightValue;
        }

        /// <summary>
        /// At this point we've determined the domino can be 
        /// played on this train.
        /// </summary>
        /// <param name="domino">Domino.</param>
        public virtual void PlayDomino(Domino domino)
        {
            if (GameManager.MasterPrivateDomino == null && domino.IsDouble)
            {
                GameManager.MasterPrivateDomino = domino;
                _train.AddFirst(domino);

                return;
            }
            else if(GameManager.MasterPrivateDomino != null && _train.Count == 0)
            {
                if (GameManager.MasterPrivateDomino.RightValue != domino.LeftValue)
                    domino.Flip();

                _train.AddFirst(domino);
                return;
            }

            // Else we have to add it to the right side
            if (_train.Last.Value.RightValue == domino.LeftValue ||
               _train.Last.Value.RightValue == domino.RightValue)
            {
                if (_train.Last.Value.RightValue != domino.LeftValue)
                    domino.Flip();

                _train.AddLast(domino);
            }
        }

        public bool IsPrivate => _isPrivate;

        public void MakePublic()
        {
            _isPrivate = false;
        }

        public virtual void MakePrivate()
        {
            _isPrivate = true;
        }

        public override string ToString()
        {
            String list = "";
            foreach (Domino d in _train)
            {
                list += d.ToString();
            }

            return list;
        }
    }

    public class GlobalPublicTrain : Train
    {
        public GlobalPublicTrain()
            : base(false)
        {   

        }

        public override void MakePrivate()
        {
            throw new NotSupportedException("Public trains cannot be made private");
        }

        public override bool CanPlayDomino(Domino domino)
        {
            return false;
        }

        public override void PlayDomino(Domino domino)
        {
            // Double can check both ends of the train...

            // Look for the correct side...if the left side is connected, 
            // we have to look at the right.
            if (_train.Last.Previous != null && _train.Last.Next == null)
            {
                if (_train.Last.Value.RightValue == domino.LeftValue ||
                   _train.Last.Value.RightValue == domino.RightValue)
                {
                    if (_train.Last.Value.LeftValue != domino.RightValue)
                        domino.Flip();

                    // Add it to the end of the train (right side).
                    _train.AddLast(domino);
                }
            }
            else if (_train.First.Previous == null && _train.First.Next != null) // Can we add it to the left side>?
            {
                if (_train.First.Value.LeftValue == domino.LeftValue ||
                   _train.First.Value.LeftValue == domino.RightValue)
                {
                    if (_train.First.Value.LeftValue != domino.RightValue)
                        domino.Flip();

                    _train.AddFirst(domino);
                }
            }
        }
    }
}
