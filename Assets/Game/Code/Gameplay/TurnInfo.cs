namespace Game.Gameplay
{
    internal readonly struct TurnInfo
    {
        public string CardId { get; }
        public string StackFromId{ get; }
        public string StackToId{ get; }

        public TurnInfo(string cardId, string stackFromId, string stackToId)
        {
            CardId = cardId;
            StackFromId = stackFromId;
            StackToId = stackToId;
        }

        public TurnInfo Reverse()
        {
            return new TurnInfo(CardId, StackToId, StackFromId);
        }
    }
}