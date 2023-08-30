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
      game.Add(0,0);
      game.FlipBoards();
      var actual = game.ToString();

      var expected = "0  \n   \n   ";

      actual.Should().Be(expected);
   }
   
   [Fact]
   public void CheckForBirth_ReturnsFalseIfCannotBirth()
   {
      var game = new TestGame(new int[3,3]{{0,0,0},{0,0,1},{0,1,0}});
      var actual = game.CheckForBirth(1,1);

      actual.Should().Be(false);
   }
   
   [Fact]
   public void CheckForBirth_ReturnsTrueIfCanBirth()
   {
      var game = new TestGame(new int[3,3]{{1,0,0},{0,0,1},{0,1,0}});
      var actual = game.CheckForBirth(1,1);

      actual.Should().Be(true);
   }
   
   [Fact]
   public void CheckForBirth_ReturnsTrueIfCanBirth_EdgeOfBoard()
   {
      var game = new TestGame(new int[3,3]{{1,0,0},{0,0,0},{0,1,1}});
      var actual = game.CheckForBirth(2,0);

      actual.Should().Be(true);
   }

   #endregion

   #region Death

   [Fact]
   public void CheckForDeath_ReturnsFalse_TwoNeighbors()
   {
      var game = new TestGame(new int[3,3] {{1,0,0},{0,1,0},{1,0,0}});
      var actual = game.CheckForDeath(1,1);

      actual.Should().Be(false);
   }

   [Fact]
   public void CheckForDeath_ReturnsFalse_ThreeNeighbors()
   {
      var game = new TestGame(new int[3,3] {{1,0,1},{0,1,0},{1,0,0}});
      var actual = game.CheckForDeath(1,1);

      actual.Should().Be(false);
   }

   [Fact]
   public void CheckForDeath_ReturnsTrue_OneNeighbor()
   {
      var game = new TestGame(new int[3,3] {{0,0,0},{0,1,0},{1,0,0}});
      var actual = game.CheckForDeath(1,1);

      actual.Should().Be(true);
   }

   [Fact]
   public void CheckForDeath_ReturnsTrue_FourNeighbors()
   {
      var game = new TestGame(new int[3,3] {{1,1,0},{0,1,0},{1,0,1}});
      var actual = game.CheckForDeath(1,1);

      actual.Should().Be(true);
   }
   
   

   [Fact]
   public void CheckForDeath_ReturnsTrue_FourNeighbors_EdgeOfBoard()
   {
      var game = new TestGame(new int[3,3] {{1,1,0},{1,1,0},{1,0,1}});
      var actual = game.CheckForDeath(2,0);

      actual.Should().Be(true);
   }

   #endregion

   #region Evolution

   [Fact]
   public void Evolve_ShouldFollowRulesOfBirthAndDeathForOneEvolution()
   {
      var game = new TestGame(new int[9, 9]
      {
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 1, 0, 1, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 1, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
      });
      
      game.Evolve();

      var expected =
         "         \n         \n         \n         \n    0    \n         \n         \n         \n         ";
      
      var actual = game.ToString();

      actual.Should().Be(expected);
   }

   [Fact]
   public void Run_ShouldFollowRulesOfBirthAndDeathForTwoEvolutions()
   {
      var game = new TestGame(new int[9, 9]
      {
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 1, 1, 1, 0, 0, 0 },
         { 0, 0, 0, 1, 0, 1, 0, 0, 0 },
         { 0, 0, 0, 1, 1, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
         { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
      });
      
      game.Run(2);
      
      var expected =
         "         \n         \n    0    \n   0 0   \n  0  0   \n   00    \n         \n         \n         ";
      
      var actual = game.ToString();

      actual.Should().Be(expected);
   }

   #endregion
}