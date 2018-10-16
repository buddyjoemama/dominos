using System;

namespace Dominoes.Trains
{
    public class PublicTrain : Train
    {
        public PublicTrain()
            : base(false, null)
        {

        }

        public override void MakePrivate()
        {
            throw new NotSupportedException("Public trains cannot be made private");
        }

        public override bool CanPlayDomino(Domino domino)
        {
          //  if (GameManager.MasterPublicDomino == null && domino.IsDouble)
          //      return true;
          //  else if (GameManager.MasterPublicDomino == null && !domino.IsDouble)
          //      return false;
          //  else if (GameManager.MasterPublicDomino != null &&
           //          _train.Count == 0 &&
           //          GameManager.MasterPublicDomino.CanAttachAny(domino))
            //{
            //    return true;
           // }

            return _train.First.Value.CanAttachLeft(domino) ||
                         _train.Last.Value.CanAttachRight(domino);
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
