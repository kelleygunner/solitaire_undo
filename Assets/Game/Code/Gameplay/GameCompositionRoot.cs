using System;
using Game.DragAndDrop;
using UnityEngine.UI;

namespace Game.Gameplay
{
    internal sealed class GameCompositionRoot : IDisposable
    {
        private readonly GameRoundController _gameRoundController;
        private readonly GameFieldView _gameFieldView;
        
        private bool _isDisposed;

        public GameCompositionRoot(GameFieldView gameFieldView)
        {
            _gameFieldView = gameFieldView;
            var dragAndDropService = DragAndDropServiceProvider.Service;
            var turnProcessor = new GameTurnProcessor(dragAndDropService);
            _gameRoundController = new GameRoundController(turnProcessor);
            gameFieldView.Initialize(_gameRoundController);
            
            _isDisposed = false;
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;
            _gameRoundController.Dispose();
            _gameFieldView.Dispose();
        }
    }
}