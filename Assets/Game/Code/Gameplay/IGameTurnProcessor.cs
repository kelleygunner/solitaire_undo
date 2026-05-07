using System;

namespace Game.Gameplay
{
    internal interface IGameTurnProcessor : IDisposable
    {
        void Subscribe(Func<TurnInfo, bool> turnHandler);
        void Unsubscribe(Func<TurnInfo, bool> turnHandler);
    }
}