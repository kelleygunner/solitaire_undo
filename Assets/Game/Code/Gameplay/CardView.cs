using Game.DragAndDrop;
using UnityEngine;

namespace Game.Gameplay
{
    internal sealed class CardView : MonoBehaviour
    {
        private IDraggableItem _draggableItemView;

        public string Id
        {
            get
            {
                if (_draggableItemView != null) 
                    return _draggableItemView.Id;
                _draggableItemView = GetComponent<IDraggableItem>();
                if (_draggableItemView == null)
                    throw new MissingComponentException("DraggableItemView is null");
                return _draggableItemView.Id;
            }
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