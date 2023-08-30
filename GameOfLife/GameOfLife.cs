namespace GameOfLife;

public class GameOfLife
{
    private int[,] _board;
    private int _width;
    private int _height;
    public GameOfLife(int width = 100, int height = 100)
    {
        _width = width;
        _height = height;
        _board = new int[width,height];
    }

    public void Run(int evolutions)
    {
        
    }

    public override string ToString()
    {
        var output = "";
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                output += _board[j, i] == 0 ? " " : "0";
            }

            if(i+1 != _height) output += "\n";
        }

        return output;
    }
}