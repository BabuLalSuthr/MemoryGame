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

    //public class ColorGameEngine
    //{
    //    private GameState gameState = GameState.Ready;
    //    private List<ColorNode> historyNodes = new();
    //    private Queue<ColorNode> historyQueue = new();

    //    private ColorGameEngine(int nodeCount, List<ColorNode> nodes, GameStateChangedEventHandler gameStateChangedEventHandler) 
    //    {
    //        NodeCount = nodeCount;
    //        Nodes = nodes;
    //        if (gameStateChangedEventHandler != null)
    //            GameStateChanged += gameStateChangedEventHandler;
    //        CurrentState = GameState.VerifyedNodes;
    //    }

    //    public int NodeCount { get; }

    //    public int Score => historyNodes?.Count ?? 0;

    //    public ColorNode LastNode { get; private set; }

    //    public List<ColorNode> Nodes { get; }

    //    public GameState CurrentState
    //    {
    //        get  => gameState;
    //        private set
    //        {
    //            if (gameState != value)
    //            {
    //                gameState = value;
    //                GameStateChanged?.Invoke(new GameStateEventArgs(gameState));
    //            }
    //        }
    //    }

    //    public static ColorGameEngine StartGame(int nodeCount, GameStateChangedEventHandler gameStateChangedEventHandler) 
    //    {
    //        List<ColorNode> nodes = new List<ColorNode>();
    //        for (int i = 0; i < nodeCount; i++) 
    //        {
    //            var newNode = GenerateUniqueRandomNode();
    //            nodes.Add(newNode);
    //        }
            
    //        return new ColorGameEngine(nodeCount, nodes, gameStateChangedEventHandler);

    //        ColorNode GenerateUniqueRandomNode()
    //        {
    //            ColorNode newNode = ColorNode.GenerateRandomNode();
    //            while (nodes.Any(n => n.Value == newNode.Value)) 
    //            {
    //                newNode = ColorNode.GenerateRandomNode();
    //            }
    //            return newNode;
    //        }
    //    }

    //    public ColorNode NextNode()
    //    {
    //        if(CurrentState == GameState.VerifyedNodes)
    //        {
    //            ColorNode randomNode = Nodes[Random.Shared.Next(Nodes.Count)];
    //            historyNodes.Add(randomNode);

    //            historyQueue = new Queue<ColorNode>(historyNodes);
    //            LastNode = randomNode;
    //            return randomNode;
    //        }
    //        throw new InvalidOperationException("Next node can show only if history nodes verified successfully.");
    //    }

    //    public void StartVerifyNodes()
    //    {
    //        CurrentState = GameState.VerifyingNodes;
    //    }

    //    public bool VerifyNextNode(Color node)
    //    {
    //        ArgumentNullException.ThrowIfNull(node);

    //        if (CurrentState == GameState.VerifyingNodes)
    //        {
    //            var dequeueNode = historyQueue.Dequeue();
    //            if (dequeueNode != null && dequeueNode.Value == node) 
    //            {
    //                if(historyQueue.Count == 0)
    //                {
    //                    CurrentState = GameState.VerifyedNodes;
    //                    NextNode();
    //                }
    //                return true;
    //            }

    //            CurrentState = GameState.Gameover;
    //            return false;
    //        }

    //        throw new InvalidOperationException("Verify node can support only for verifying node state.");
    //    }

    //    public event GameStateChangedEventHandler GameStateChanged;
    //}

}
