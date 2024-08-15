namespace MemoryGame.Core.Engines
{
    public class NumberMemoryGameEngine : MemoryGameEngine<int>, INumberMemoryGameEngine
    {
        public NumberMemoryGameEngine(int nodeCount, GameStateChangedEventHandler gameStateChangedEventHandler) : base(nodeCount, gameStateChangedEventHandler)
        {
        }

        protected override int GenerateRandomNode()
        {
            return Random.Shared.Next(100);
        }

        public static NumberMemoryGameEngine StartGame(int nodeCount, GameStateChangedEventHandler gameStateChangedEventHandler)
        {
            return new NumberMemoryGameEngine(nodeCount, gameStateChangedEventHandler);
        }
    }
}
