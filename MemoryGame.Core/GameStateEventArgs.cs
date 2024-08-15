namespace MemoryGame.Core
{
    public class GameStateEventArgs : EventArgs
    {
        public GameStateEventArgs(GameState gameState)
        {
            GameState = gameState;
        }

        public GameState GameState { get; }
    }

    public delegate void GameStateChangedEventHandler(GameStateEventArgs e);

}
