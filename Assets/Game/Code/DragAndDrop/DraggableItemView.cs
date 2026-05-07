using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.DragAndDrop
{
    [DisallowMultipleComponent]
    internal sealed class DraggableItemView : MonoBehaviour, IDraggableItem,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private string _id;
        [SerializeField] private Camera _raycastCamera;
        [SerializeField] private DraggableDockView _startDock;
        [SerializeField] private Collider2D _collider;

        private Vector3 _startPosition;
        private Vector3 _pointerOffset;
        private string _fromDock;

        public string Id => _id;
        
        private string _currentDock;

        private void Awake()
        {
            if (_raycastCamera == null)
                _raycastCamera = Camera.main;
            if (_startDock != null)
                _currentDock = _startDock.Id;
        }

        public void SetCurrentDock(string dock)
        {
            _currentDock = dock;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = transform.position;
            _fromDock = _currentDock;

            var pointerWorldPosition = GetPointerWorldPosition(eventData);
            _pointerOffset = transform.position - pointerWorldPosition;

            DragAndDropServiceProvider.Service.StartDrag(this, _fromDock);
            _collider.enabled = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = GetPointerWorldPosition(eventData) + _pointerOffset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var targetDock = FindDockUnderPointer(eventData);

            var hasValidTarget =
                targetDock != null &&
                _fromDock != null &&
                targetDock.Id != _fromDock;
            
            var isDragConfirmed = hasValidTarget && DragAndDropServiceProvider.Service.CompleteDrag(this, _fromDock, targetDock.Id);

            if (isDragConfirmed)
            {
                _currentDock = targetDock.Id;
            }
            else
            {
                transform.position = _startPosition;
                DragAndDropServiceProvider.Service.CancelDrag(this, _fromDock);
            }

            _fromDock = null;
            _collider.enabled = true;
        }

        private Vector3 GetPointerWorldPosition(PointerEventData eventData)
        {
            Vector3 screenPosition = eventData.position;
            screenPosition.z = -_raycastCamera.transform.position.z;

            var worldPosition = _raycastCamera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = transform.position.z;

            return worldPosition;
        }

        private static IDraggableDock FindDockUnderPointer(PointerEventData eventData)
        {
            foreach (var result in eventData.hovered)
            {
                if (result.TryGetComponent(out DraggableDockView dock))
                    return dock;

                dock = result.GetComponentInParent<DraggableDockView>();
                if (dock != null)
                    return dock;
            }

            return null;
        }
    }
}