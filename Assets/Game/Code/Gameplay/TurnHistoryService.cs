using System.Collections.Generic;

namespace Game.Gameplay
{
    internal class TurnHistoryService
    {
        private const int MaxUndoCount = 1;
        
        private readonly Stack<TurnInfo> _turnHistory = new ();
        private int _undoCounter;
        
        public bool CanUndo => _turnHistory.Count > 0 && _undoCounter < MaxUndoCount;
        
        public void AddTurn(TurnInfo turnInfo)
        {
            _undoCounter = 0;
            _turnHistory.Push(turnInfo);
        }

        public TurnInfo? UndoLastTurn()
        {
            if (!CanUndo || _turnHistory.Count == 0)
                return null;
            _undoCounter++;
            return _turnHistory.Pop();
        }
    }
}
