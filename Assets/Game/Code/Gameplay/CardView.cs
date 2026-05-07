using Game.DragAndDrop;
using UnityEngine;

namespace Game.Gameplay
{
    internal sealed class CardView : MonoBehaviour
    {
        private IDraggableItem _draggableItemView;
        public string Id => _draggableItemView.Id;

        private void Awake()
        {
            _draggableItemView = GetComponent<IDraggableItem>();
            if (_draggableItemView == null)
                throw new MissingComponentException("DraggableItemView is null");
        }

        public void SetStack(string newStackId)
        {
            _draggableItemView.SetCurrentDock(newStackId);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}