using System;
using Game.DragAndDrop;

namespace Game.Gameplay
{
    internal sealed class GameTurnProcessor : IGameTurnProcessor
    {
        private readonly DragAndDropService _dragAndDropService;
        private bool _isDisposed;
        private Func<TurnInfo, bool> _turnHandler;
        
        public GameTurnProcessor(DragAndDropService dragAndDropService)
        {
            _dragAndDropService = dragAndDropService;
            _dragAndDropService.DragRequest += OnDragRequest;
            _isDisposed = false;
        }

        private bool OnDragRequest(DragInfo dragInfo)
        {
            if (_turnHandler == null)
                return false;
            var turnInfo = new TurnInfo(dragInfo.Item.Id, dragInfo.From, dragInfo.To);
            return _turnHandler.Invoke(turnInfo);
        }

        public void Subscribe(Func<TurnInfo, bool> turnHandler)
        {
            // There can be only one subscriber, so we can just replace the handler
            _turnHandler = turnHandler;
        }

        public void Unsubscribe(Func<TurnInfo, bool> turnHandler)
        {
            _turnHandler = null;
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _dragAndDropService.DragRequest -= OnDragRequest;
            _isDisposed = true;
        }
    }
}