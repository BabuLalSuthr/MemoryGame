using MemoryGame.Core.Engines;
using Xunit;

namespace MemoryGame.Core.Tests.Engines
{
    public class ColorMemoryGameEngineTest
    {
        [Fact]
        public void ColorMemoryGameEngine_StartGame_ShouldReturnInstance()
        {
            //Arrenge
            int totalNodes = 4;
            GameStateChangedEventHandler gameStateChangedEventHandler = GameStateChanged;

            //Act
            var newGameEngine = ColorMemoryGameEngine.StartGame(totalNodes, gameStateChangedEventHandler);

            //Asserts
            Assert.NotNull(newGameEngine);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        public void ColorMemoryGameEngine_StartGame_ShouldMatchTotalNodes(int totalNodes)
        {
            //Arrenge
            GameStateChangedEventHandler gameStateChangedEventHandler = GameStateChanged;

            //Act
            var newGameEngine = ColorMemoryGameEngine.StartGame(totalNodes, gameStateChangedEventHandler);

            //Asserts
            Assert.Equal(totalNodes, newGameEngine.Nodes.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(11)]
        [InlineData(12)]
        public void ColorMemoryGameEngine_StartGame_ShouldThrowArgumentException_IfTotalNodesLessThen2_OrGreaterThen10(int totalNodes)
        {
            //Arrenge
            GameStateChangedEventHandler gameStateChangedEventHandler = GameStateChanged;

            //Act & Assert
            Assert.Throws<ArgumentException>(() => 
            {
                var newGameEngine = ColorMemoryGameEngine.StartGame(totalNodes, gameStateChangedEventHandler);
            });
        }

        #region Helper Methods

        void GameStateChanged(GameStateEventArgs e)
        {

        } 

        #endregion
    }
}