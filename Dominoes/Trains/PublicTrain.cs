using System;
using Dominoes.Players;

namespace Dominoes.Trains
{
    public class PublicTrain : Train
    {
        public PublicTrain()
            : base(false, null, null)
        {

        }

        public override void MakePrivate()
        {
            throw new NotSupportedException("Public trains cannot be made private");
        }

        public override bool CanPlayDomino(Domino domino)
        {
            if (_train.Count == 0 && domino.IsDouble)
                return true;
            else if (_train.Count == 0 && !domino.IsDouble)
                return false;

            return _train.First.Value.CanAttachLeft(domino) ||
                         _train.Last.Value.CanAttachRight(domino);
        }

        public override void PlayDomino(Domino domino)
        {
            if(_train.Count == 0 && domino.IsDouble)
            {
                _train.AddFirst(domino);

            }

            // Double can check both ends of the train...
            // Look for the correct side...if the left side is connected, 
            // we have to look at the right.
            else if(_train.First.Value.CanAttachLeft(domino)) {
                if (_train.First.Value.LeftValue != domino.RightValue)
                    domino.Flip();

                _train.AddFirst(domino);
            }
            else if(_train.Last.Value.CanAttachRight(domino))
            {
                if (_train.Last.Value.RightValue != domino.LeftValue)
                    domino.Flip();

                _train.AddLast(domino);
            }
        }

        public override bool IsMyTrain(Player player)
        {
            return false;
        }
    }
}
