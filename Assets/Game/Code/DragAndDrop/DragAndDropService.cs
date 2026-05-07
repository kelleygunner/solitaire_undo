using System;

namespace Game.DragAndDrop
{
    public sealed class DragAndDropService
    {
        public event Action<DragInfo> DragStarted;
        public event Func<DragInfo, bool> DragRequest;
        public event Action<DragInfo> DragCanceled;

        internal void StartDrag(IDraggableItem item, string from)
        {
            DragStarted?.Invoke(new DragInfo(item, from, null));
        }

        internal bool CompleteDrag(IDraggableItem item, string from, string to)
        {
            return DragRequest != null && DragRequest.Invoke(new DragInfo(item, from, to));
        }

        internal void CancelDrag(IDraggableItem item, string from)
        {
            DragCanceled?.Invoke(new DragInfo(item, from, null));
        }
    }
}