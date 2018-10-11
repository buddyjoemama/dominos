using System;
using System.Collections.Generic;

namespace Dominoes
{
    public class Train
    {
        private LinkedList<Domino> _train = new LinkedList<Domino>();
        private bool _isPrivate = false;

        // Double ended train.
        private bool _isDouble = false;

        public Train(Domino master)
        {
            _train.AddFirst(master);
        }

        public bool CanPlayDomino(Domino domino)
        {
            // Always play when the train is empty
            if(_train.Count == 0)
            {
                return true;
            }

            var first = _train.First;
            var last = _train.Last;

            // At the head of the train....either end can work.
            if(_train.Count == 1)
            {
                return first.Value.LeftValue == domino.LeftValue ||
                            first.Value.RightValue == domino.RightValue;
            }

            // Can go on either end.
            if (_isDouble)
            {
                return first.Value.LeftValue == domino.LeftValue ||
                            first.Value.LeftValue == domino.RightValue ||
                            last.Value.LeftValue == domino.LeftValue ||
                            last.Value.RightValue == domino.RightValue;
            }
            else 
            {
                // Can it go on the end.
                return last.Value.RightValue == domino.LeftValue ||
                           last.Value.RightValue == domino.RightValue;
            }
        }

        /// <summary>
        /// At this point we've determined the domino can be 
        /// played on this train.
        /// </summary>
        /// <param name="domino">Domino.</param>
        public void PlayDomino(Domino domino)
        {
            if (_isDouble)
            {
                // Double can check both ends of the train...

                // Look for the correct side...if the left side is connected, 
                // we have to look at the right.
                if (_train.Last.Previous != null && _train.Last.Next == null)
                {
                    if(_train.Last.Value.RightValue == domino.LeftValue ||
                       _train.Last.Value.RightValue == domino.RightValue)
                    {   
                        if(_train.Last.Value.LeftValue != domino.RightValue)
                            domino.Flip();

                        // Add it to the end of the train (right side).
                        _train.AddLast(domino);
                    }
                }
                else  if(_train.First.Previous == null && _train.First.Next != null) // Can we add it to the left side>?
                {
                    if(_train.First.Value.LeftValue == domino.LeftValue ||
                       _train.First.Value.LeftValue == domino.RightValue)
                    {
                        if (_train.First.Value.LeftValue != domino.RightValue)
                            domino.Flip();

                        _train.AddFirst(domino);
                    }
                }
            }
            else
            {
                // Else we have to add it to the right side
                if(_train.Last.Value.RightValue == domino.LeftValue ||
                   _train.Last.Value.RightValue == domino.RightValue)
                {
                    if (_train.Last.Value.RightValue != domino.LeftValue)
                        domino.Flip();

                    _train.AddLast(domino);
                }
            }
        }

        public bool IsPrivate => _isPrivate;

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
}
