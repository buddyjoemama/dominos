using System;
using System.Collections.Generic;
using Dominoes.Trains;

namespace Dominoes.Players.Strategy
{
    public interface IStrategy
    {
        bool CanPlay(List<Domino> myDominoes, Train myTrain);
        void Play(List<Domino> myDominoes, Train train);
    }
}
