namespace MemoryGame.Core
{
    public interface IMemoryGameEngine<T>
        where T : IEquatable<T>
    {
        int NodeCount { get; }

        IReadOnlyList<T> Nodes { get; }

        int Score { get; }

        T LastNode { get; }

        GameState CurrentState { get; }

        T NextNode();

        void StartVerifyNodes();

        bool VerifyNextNode(T node);

        event GameStateChangedEventHandler GameStateChanged;
    }
}
