namespace UnitTests;

using Game = GameOfLife.GameOfLife;
using TestGame = GameOfLife.TestGameOfLife;

public class GameOfLifeTests
{
   #region Initialization

   [Fact]
   public void GameInitializesCorrectly()
   {
      var actual = new Game();

      var actualLines = actual.ToString().Split("\n");
      
      actualLines.Length.Should().Be(100);
      actualLines[0].Length.Should().Be(100);
      actualLines[0].Should()
         .Be("                                                                                                    ");
   }
   
   [Fact]
   public void GameInitializesCorrectly_9x9()
   {
      var actual = new Game(9,9);

      var actualLines = actual.ToString().Split("\n");
      
      actualLines.Length.Should().Be(9);
      actualLines[0].Length.Should().Be(9);
      actualLines[0].Should()
         .Be("         ");
   }

   #endregion

   #region Birth

   [Fact]
   public void Birth()
   {
      var game = new Game(3, 3);
      game.Birth(0,0);

      var actual = game.ToString();

      var expected = "0  \n   \n   ";

      actual.Should().Be(expected);
   }
   
   [Fact]
   public void CheckForBirth_ReturnsFalseIfCannotBirth()
   {
      var game = new TestGame(new int[3,3]{{0,0,0},{0,0,1},{0,1,0}});
      var actual = game.ChechForBirth(1,1);

      actual.Should().Be(false);
   }
   
   [Fact]
   public void CheckForBirth_ReturnsTrueIfCanBirth()
   {
      var game = new TestGame(new int[3,3]{{1,0,0},{0,0,1},{0,1,0}});
      var actual = game.ChechForBirth(1,1);

      actual.Should().Be(true);
   }

   #endregion
}