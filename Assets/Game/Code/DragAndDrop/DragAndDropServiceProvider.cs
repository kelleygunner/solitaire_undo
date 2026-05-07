namespace Game.DragAndDrop
{
    public static class DragAndDropServiceProvider
    {
        // Singleton instance of the DragAndDropService
        // Just for simplicity, we are using a static instance here.
        // In a more complex application, you might want to use a dependency injection framework.
        public static readonly DragAndDropService Service = new();
    }
}