using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay
{
    internal sealed class GameFieldView : MonoBehaviour, IDisposable
    {
        [SerializeField] private CardView[] _cardViews;
        [SerializeField] private StackView[] _stackViews;
        
        // Just to demonstrate the undo functionality, in a real game this would be part of a more complex UI
        [SerializeField] private Button _undoButton;

        private readonly Dictionary<string, CardView> _cardViewDictionary = new();
        private readonly Dictionary<string, StackView> _stackViewDictionary = new();

        private GameRoundController _gameRoundController;
        private bool _isDisposed;

        public void Initialize(GameRoundController gameRoundController)
        {
            foreach (var card in _cardViews)
            {
                _cardViewDictionary.TryAdd(card.Id, card);
            }

            foreach (var stack in _stackViews)
            {
                _stackViewDictionary.TryAdd(stack.Id, stack);
            }
            
            _gameRoundController = gameRoundController;   
            _gameRoundController.OnTurnComplete += OnTurnComplete;
            _gameRoundController.OnTurnUndone += OnTurnComplete;
            _undoButton.onClick.AddListener(_gameRoundController.UndoLastTurn);
            _isDisposed = false;
            UpdateUi();
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;
            if (_gameRoundController == null)
                return;
            _gameRoundController.OnTurnComplete -= OnTurnComplete;
            _gameRoundController.OnTurnUndone -= OnTurnComplete;
            _undoButton.onClick.RemoveListener(_gameRoundController.UndoLastTurn);
        }

        private void OnTurnComplete(TurnInfo turnInfo)
        {
            if(!_cardViewDictionary.TryGetValue(turnInfo.CardId, out var card))
                return;

            if (!_stackViewDictionary.TryGetValue(turnInfo.StackToId, out var stack))
                return;
            
            var position = stack.Position;
            card.SetPosition(position);
            card.SetStack(turnInfo.StackToId);
            UpdateUi();
        }

        // In a real game, the UI would likely be updated in a more efficient way, but this is just to demonstrate the undo functionality
        private void UpdateUi()
        {
            _undoButton.interactable = _gameRoundController.CanUndo;
        }
    }
}