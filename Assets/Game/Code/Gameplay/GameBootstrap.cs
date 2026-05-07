using UnityEngine;

namespace Game.Gameplay
{
    internal sealed class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private GameFieldView _gameFieldView;

        private GameCompositionRoot _gameCompositionRoot;

        private void Awake()
        {
            _gameCompositionRoot = new GameCompositionRoot(_gameFieldView);
        }

        private void OnDestroy()
        {
            _gameCompositionRoot.Dispose();
        }
    }
}