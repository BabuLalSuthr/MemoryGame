using System.Drawing;

namespace MemoryGame.Core
{
    public abstract class MemoryGameEngine<T> : IMemoryGameEngine<T>
            where T : IEquatable<T>
    {
        private GameState gameState = GameState.Ready;
        private List<T> historyNodes = new();
        private Queue<T> historyQueue = new();

        protected MemoryGameEngine(int nodeCount, GameStateChangedEventHandler gameStateChangedEventHandler)
        {
            if(nodeCount < 2 ||  nodeCount > 10)
                throw new ArgumentException("Node count must be between 2 to 10.", nameof(nodeCount));

            NodeCount = nodeCount;

            List<T> nodes = new List<T>();
            for (int i = 0; i < nodeCount; i++)
            {
                var newNode = GenerateUniqueRandomNode();
                nodes.Add(newNode);
            }

            Nodes = nodes.AsReadOnly();
            if (gameStateChangedEventHandler != null)
                GameStateChanged += gameStateChangedEventHandler;
            CurrentState = GameState.VerifyedNodes;
        }
        
        #region Properties

        public int NodeCount { get; }

        public int Score => historyNodes?.Count ?? 0;

        public T LastNode { get; private set; }

        public IReadOnlyList<T> Nodes { get; } = [];

        public GameState CurrentState
        {
            get => gameState;
            private set
            {
                if (gameState != value)
                {
                    gameState = value;
                    GameStateChanged?.Invoke(new GameStateEventArgs(gameState));
                }
            }
        }

        #endregion

        #region Methods

        public T NextNode()
        {
            if (CurrentState == GameState.VerifyedNodes)
            {
                T randomNode = Nodes[Random.Shared.Next(Nodes.Count)];
                historyNodes.Add(randomNode);

                historyQueue = new Queue<T>(historyNodes);
                LastNode = randomNode;
                return randomNode;
            }
            throw new InvalidOperationException("Next node can show only if history nodes verified successfully.");
        }

        public void StartVerifyNodes()
        {
            CurrentState = GameState.VerifyingNodes;
        }

        public bool VerifyNextNode(T node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (CurrentState == GameState.VerifyingNodes)
            {
                var dequeueNode = historyQueue.Dequeue();
                if (dequeueNode != null && node.Equals(dequeueNode))
                {
                    if (historyQueue.Count == 0)
                    {
                        CurrentState = GameState.VerifyedNodes;
                        NextNode();
                    }
                    return true;
                }

                CurrentState = GameState.Gameover;
                return false;
            }

            throw new InvalidOperationException("Verify node can support only for verifying node state.");
        }

        private T GenerateUniqueRandomNode()
        {
            var newColor = GenerateRandomNode();
            while (Nodes.Any(n => n.Equals(newColor)))
            {
                newColor = GenerateRandomNode();
            }
            return newColor;
        }

        protected abstract T GenerateRandomNode();

        #endregion

        public event GameStateChangedEventHandler GameStateChanged;
    }
}
