namespace Game.DragAndDrop
{
    public interface IDraggableItem
    {
        string Id { get; }
        void SetCurrentDock(string dockId);
    }
}