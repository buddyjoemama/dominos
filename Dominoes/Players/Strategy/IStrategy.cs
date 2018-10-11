using System;
using System.Collections.Generic;

namespace Dominoes.Players.Strategy
{
    public interface IStrategy
    {
        bool CanPlay(List<Domino> myDominoes, Train myTrain);
        void Play(List<Domino> myDominoes, Train train);
    }
}
