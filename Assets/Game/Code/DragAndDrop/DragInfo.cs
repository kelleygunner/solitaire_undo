namespace Game.DragAndDrop
{
    public readonly struct DragInfo
    {
        public readonly IDraggableItem Item;
        public readonly string From;
        public readonly string To;

        public DragInfo(IDraggableItem item, string from, string to)
        {
            Item = item;
            From = from;
            To = to;
        }
    }
}