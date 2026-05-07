using System;
using UnityEngine;

namespace Game.Gameplay
{
    internal sealed class GameRoundController : IDisposable
    {
        private bool _isDisposed;
        private readonly IGameTurnProcessor _gameTurnProcessor;
        private readonly TurnHistoryService _turnHistoryService = new();

        public event Action<TurnInfo> OnTurnComplete;
        public event Action<TurnInfo> OnTurnUndone;
        
        public bool CanUndo => _turnHistoryService.CanUndo;
        
        public GameRoundController(IGameTurnProcessor gameTurnProcessor)
        {
            _gameTurnProcessor = gameTurnProcessor;
            _gameTurnProcessor.Subscribe(ConfirmTurn);
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;
            _gameTurnProcessor.Unsubscribe(ConfirmTurn);
            _gameTurnProcessor.Dispose();
        }
        
        public void UndoLastTurn()
        {
            if (!_turnHistoryService.CanUndo)
                return;
            var turnInfo = _turnHistoryService.UndoLastTurn();
            if (turnInfo == null)
                return;
            var turnReverseInfo = turnInfo.Value.Reverse();
            OnTurnUndone?.Invoke(turnReverseInfo);
            
            Debug.Log($"{turnReverseInfo.CardId} returned {turnReverseInfo.StackFromId} -> {turnReverseInfo.StackToId}");
        }

        private bool ConfirmTurn(TurnInfo turnInfo)
        {
            _turnHistoryService.AddTurn(turnInfo);
            OnTurnComplete?.Invoke(turnInfo);
            
            Debug.Log($"{turnInfo.CardId} moved {turnInfo.StackFromId} -> {turnInfo.StackToId}");
            // There is no game logic implemented, so we just confirm all turns
            return true;
        }
    }
}