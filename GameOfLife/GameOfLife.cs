namespace GameOfLife;

public class GameOfLife
{
    protected int[,] _board;
    protected int _width;
    protected int _height;
    public GameOfLife(int width = 100, int height = 100)
    {
        _width = width;
        _height = height;
        _board = new int[width,height];
    }

    public void Run(int evolutions)
    {
        throw new NotImplementedException();
    }

    public void Birth(int x, int y)
    {
        _board[x, y] = 1;
    }

    public bool ChechForBirth(int x, int y)
    {
        var left = x - 1 == -1 ? _width - 1 : x-1;
        var right = x + 1 == _width ? 0 : x+1;
        var up = y - 1 == -1 ? _height - 1 : y-1;
        var down = y + 1 == _height ? 0 : y+1;

        var relaventNeighbors = new List<Pair>
        {
            new(left, up),
            new(left, y),
            new(left, down),
            new(x, up),
            new(x, down),
            new(right, up),
            new(right, y),
            new(right, down)
        };

        return relaventNeighbors.Count(p => _board[p.X, p.Y] == 1) == 3;
    }

    public void Kill(int x, int y)
    {
        throw new NotImplementedException();
    }

    public bool CheckForDeath(int x, int y)
    {
        throw new NotImplementedException();
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

public class Pair
{
    public int X { get; }
    public int Y { get; }

    public Pair(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class TestGameOfLife : GameOfLife
{
    public TestGameOfLife(int[,] board) : base()
    {
        _width = board.GetLength(0);
        _height = board.GetLength(1);
        _board = board;
    }
}