using Game.DragAndDrop;
using UnityEngine;

namespace Game.Gameplay
{
    public class StackView : MonoBehaviour
    {
        private const float ZOffset = 0.1f;
        private IDraggableDock _draggableDockView;

        private void Awake()
        {
            _draggableDockView = GetComponent<IDraggableDock>();
            if (_draggableDockView == null)
                throw new MissingComponentException("DraggableDockView is null");
        }

        public string Id => _draggableDockView.Id;

        public Vector3 Position => transform.position - Vector3.forward * ZOffset;
    }
}