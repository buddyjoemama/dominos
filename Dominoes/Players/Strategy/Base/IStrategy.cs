using System;
using System.Collections.Generic;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public interface IStrategy
    {
        (bool canPlay, List<Domino> nextToPlay) CanPlay(List<Domino> myDominoes, Train myTrain);
        void Play(List<Domino> playList, List<Domino> playersDominos, Train trainToPlayOn);
    }
}
