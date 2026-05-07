using UnityEngine;

namespace Game.DragAndDrop
{
    [DisallowMultipleComponent]
    internal sealed class DraggableDockView : MonoBehaviour, IDraggableDock
    {
        [SerializeField] private string _id;

        public string Id => _id;
    }
}