namespace UnitTests;

public class GameOfLifeTests
{
   [Fact]
   public void GameInitializesCorrectly()
   {
      var actual = new GameOfLife.GameOfLife();

      var actualLines = actual.ToString().Split("\n");
      
      actualLines.Length.Should().Be(100);
      actualLines[0].Length.Should().Be(100);
      actualLines[0].Should()
         .Be("                                                                                                    ");
   }
}