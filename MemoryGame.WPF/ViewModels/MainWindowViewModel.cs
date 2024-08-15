using MemoryGame.Core;
using MemoryGame.Core.Engines;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Windows.Threading;

namespace MemoryGame.WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<ColorNodeViewModel> colorNodeViewModels = new ObservableCollection<ColorNodeViewModel>();
        private ObservableCollection<ColorNodeViewModel> colorNodeHistoryViewModels = new ObservableCollection<ColorNodeViewModel>();
        private List<Color> historyColor = new List<Color>();
        private GameState gameState;

        public MainWindowViewModel()
        {
            StartGameCommand = new DelegateCommand(StartGame);
            VereifyColorCommand = new DelegateCommand<ColorNodeViewModel>(VereifyColor);
            StartVerifyNodesCommand = new DelegateCommand(StartVerifyNodes);
        }

        private void StartVerifyNodes()
        {
            ColorGameEngine.StartVerifyNodes();
        }

        private int totalNodes;

        public int TotalNodes
        {
            get { return totalNodes; }
            set
            {
                if (value < 2)
                    value = 2;
                else if (value > 10)
                    value = 10;
                SetProperty(ref totalNodes, value);
            }
        }

        public int Score => ColorGameEngine?.Score ?? 0;

        private ColorMemoryGameEngine ColorGameEngine { get; set; }

        public DelegateCommand StartGameCommand { get; set; }

        public DelegateCommand StartVerifyNodesCommand { get; set; }

        public DelegateCommand<ColorNodeViewModel> VereifyColorCommand { get; set; }

        public ObservableCollection<ColorNodeViewModel> ColorNodeViewModels
        {
            get => colorNodeViewModels;
            set => SetProperty(ref colorNodeViewModels, value);
        }

        public ObservableCollection<ColorNodeViewModel> ColorNodeHistoryViewModels
        {
            get => colorNodeHistoryViewModels;
            set => SetProperty(ref colorNodeHistoryViewModels, value);
        }

        public GameState GameState
        {
            get { return gameState; }
            set
            {
                if (SetProperty(ref gameState, value))
                {
                    RaisePropertyChanged(nameof(IsGameStateReady));
                    RaisePropertyChanged(nameof(IsGameStateVerifyedNodes));
                    RaisePropertyChanged(nameof(IsGameStateVerifyingNodes));
                    RaisePropertyChanged(nameof(IsGameStateGameover));
                }
            }
        }

        public bool IsGameStateReady
        {
            get => gameState == GameState.Ready;
        }

        public bool IsGameStateVerifyedNodes
        {
            get => gameState == GameState.VerifyedNodes;
        }

        public bool IsGameStateVerifyingNodes
        {
            get => gameState == GameState.VerifyingNodes;
        }

        public bool IsGameStateGameover
        {
            get => gameState == GameState.Gameover;
        }

        #region Methods

        private void StartGame()
        {
            ColorGameEngine = ColorMemoryGameEngine.StartGame(TotalNodes, GameStateChangedHandler);
            ColorNodeViewModels = new ObservableCollection<ColorNodeViewModel>();
            foreach (var node in ColorGameEngine.Nodes)
            {
                ColorNodeViewModels.Add(new ColorNodeViewModel(node));
            }
            ShowNextNode();
        }

        private void ShowNextNode()
        {
            ColorNodeHistoryViewModels.Clear();
            var nextNode = ColorGameEngine.NextNode();
            ActiveNextNode(nextNode);
        }

        private void ActiveNextNode(System.Drawing.Color nextNode)
        {
            foreach (var node in ColorNodeViewModels)
            {
                node.IsActive = node.FillColor == nextNode;
            }
            RaisePropertyChanged(nameof(Score));
        }

        private void VereifyColor(ColorNodeViewModel model)
        {
            if (ColorGameEngine.VerifyNextNode(model.FillColor))
            {
                ColorNodeHistoryViewModels.Add(model);

                if (GameState == GameState.VerifyedNodes)
                {
                    ColorNodeHistoryViewModels.Clear();
                    var nextNode = ColorGameEngine.LastNode;
                    ActiveNextNode(nextNode);
                }
            }
        }

        private void GameStateChangedHandler(GameStateEventArgs e)
        {
            GameState = e.GameState;
        }

        #endregion
    }
}
