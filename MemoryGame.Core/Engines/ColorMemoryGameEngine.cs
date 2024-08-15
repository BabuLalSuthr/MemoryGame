using System.Drawing;

namespace MemoryGame.Core.Engines
{
    public class ColorMemoryGameEngine : MemoryGameEngine<Color>, IColorMemoryGameEngine
    {
        private ColorMemoryGameEngine(int nodeCount, GameStateChangedEventHandler gameStateChangedEventHandler) : base(nodeCount, gameStateChangedEventHandler)
        {

        }

        protected override Color GenerateRandomNode()
        {
            return Color.FromArgb(Random.Shared.Next(25) * 10, Random.Shared.Next(25) * 10, Random.Shared.Next(25) * 10);
        }

        public static ColorMemoryGameEngine StartGame(int nodeCount, GameStateChangedEventHandler gameStateChangedEventHandler)
        {
            return new ColorMemoryGameEngine(nodeCount, gameStateChangedEventHandler);
        }
    }
}
