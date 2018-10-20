using System;
using System.Collections.Generic;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public interface IStrategy
    {
        (bool canPlay, List<Domino> nextToPlay, Train trainToPlay) CanPlay(List<Domino> myDominoes, Train myTrain, Player player);
        void Play(List<Domino> playList, List<Domino> playersDominos, Train trainToPlayOn);
    }
}
