using System;
using System.Collections.Generic;
using System.Linq;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    /// <summary>
    /// Build private train, dont use private unless need to.
    /// Preferentially use public.
    /// </summary>
    public class EricsStrategy : TopDownPublicExclusiveStrategy
    {
        // Our internal strategy will be to delegate to the top down private
        // to build candidate trains.
        private IStrategy privateStrategy = new TopDownPrivateExclusiveStrategy();
        private Train _privateTrain = null;

        public override (bool canPlay, List<Domino> nextToPlay, Train trainToPlay) 
            CanPlay(List<Domino> myDominoes, Train myTrain, Player player)
        {
            List<Train> candidateTrains = BuildTrainsFrom(myDominoes, 
                                                          myTrain.FirstDomino,
                                                          player);

            // For all the candidate trains, find the one with the most points...
            // this will be used as the internal private train.
            if (candidateTrains.Count > 0)
            {
                int maxDots = candidateTrains.Max(s => s.TotalDots);
                _privateTrain = candidateTrains
                    .First(s => s.TotalDots == maxDots);

                myDominoes = myDominoes.Minus(_privateTrain);
            }

            // Can we play any of our "public" dominos??
            if (base.CanPlay(myDominoes, myTrain, player).canPlay)
                return base.CanPlay(myDominoes, myTrain, player);

            // No? Start playing the on the private train from the private train stash
            var headOfPrivate = _privateTrain.Pop();

            // Add it back to the main list and call the base...it will be removed on play()
            myDominoes.Add(headOfPrivate);

            return base.CanPlay(myDominoes, myTrain, player);
        }

        public override void Pick(List<Domino> playersDominos, DominoList sourceList)
        {
            Domino domino = sourceList.TakeNextAvailable();

            if (_privateTrain != null && _privateTrain.CanPlayDomino(domino))
                _privateTrain.PlayDomino(domino);
            else
                playersDominos.Add(domino);
        }

        /// <summary>
        /// Builds a list of candidate trains which will be used for the private set.
        /// </summary>
        /// <returns>The trains from.</returns>
        /// <param name="myDominoes">My dominoes.</param>
        /// <param name="firstDomino">First domino.</param>
        /// <param name="player">Player.</param>
        private List<Train> BuildTrainsFrom(List<Domino> myDominoes, Domino firstDomino, Player player)
        {
            // Find the dominos that can be attached to the first domino
            var subTrains = myDominoes.Where(s => s.CanAttachLeft(firstDomino))
                                      .Concat(myDominoes.Where(s=>s.CanAttachRight(firstDomino)).Each(s=> 
                                        {
                                            s.Flip();
                                            return s;
                                        }))
                                      .ToList();

            List<Train> sub = new List<Train>();
            foreach(var subTrainHead in subTrains)
            {
                List<Domino> list = new List<Domino>(myDominoes.Without(subTrainHead));
                Train newTrain = new Train(true, subTrainHead, null);

                CompleteTrainFrom(list, newTrain);

                if (!newTrain.IsEmpty)
                    sub.Add(newTrain);
            }

            return sub;
        }

        /// <summary>
        /// Recursively build a train from the specified set of dominos.
        /// </summary>
        /// <param name="dominos">Dominos.</param>
        /// <param name="trainToBuild">Train to build.</param>
        public void CompleteTrainFrom(List<Domino> dominos, Train trainToBuild)
        {
            foreach(var domino in dominos.OrderByDescending(s=>s.Sum).ToList())
            {
                var cPlay = privateStrategy.CanPlay(dominos, trainToBuild, null);

                if(cPlay.canPlay)
                {
                    foreach (var dominoToPlay in cPlay.nextToPlay)
                    {
                        trainToBuild.PlayDomino(dominoToPlay);
                        dominos.Remove(dominoToPlay);
                    }

                    CompleteTrainFrom(dominos, trainToBuild);
                }   
            }
        }
    }
}

// Root = [1,1]

/* Subtrains 
[1,2]
[1,4]
[1,6]
*/

// Dominos = [2,3], [2,7], [3,11], [5,6], [8,8], [9,2], [4,4], [6,3], [5, 11], [7,4], [8,5]
