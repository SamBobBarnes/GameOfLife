using System.Runtime.InteropServices;

namespace GameOfLife;

public class GameOfLife
{
    protected int[,] _board;
    private int[,] _tempBoard;
    protected int _width;
    protected int _height;
    public GameOfLife(int width, int height)
    {
        _width = width;
        _height = height;
        _board = new int[height,width];
        ResetTempBoard();
    }

    public GameOfLife()
    {
        GetSeed();
        ResetTempBoard();
    }

    private void GetSeed()
    {
        var input = System.IO.File.ReadAllText(@"../../../seed.txt");
        input = input.Replace("\r", "");
        var lines = input.Split("\n");
        _width = lines[0].Length;
        _height = lines.Length;
        _board = new int[_height, _width];
        for (int i = 0; i < _height; i++)
        {
            var row = lines[i].ToCharArray();
            for (int j = 0; j < _width; j++)
            {
                _board[i, j] = row[j] == '1' ? 1 : 0;
            }
        }
    }

    public void Run(int evolutions)
    {
        Console.WriteLine(ToString());
        for (int i = 0; i < evolutions; i++)
        {
            Evolve();
            Console.Clear();
            Console.WriteLine(ToString());
            Thread.Sleep(1000);
        }
    }

    public void FlipBoards()
    {
        _board = _tempBoard;
        ResetTempBoard();
    }

    protected void ResetTempBoard()
    {
        _tempBoard = new int[_height, _width];
    }
    
    private int GetNeighbors(int x, int y)
    {
        var left = x - 1 == -1 ? _width - 1 : x - 1;
        var right = x + 1 == _width ? 0 : x + 1;
        var up = y - 1 == -1 ? _height - 1 : y - 1;
        var down = y + 1 == _height ? 0 : y + 1;

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

        return relaventNeighbors.Count(p => _board[p.Y, p.X] == 1);
    }

    public void Evolve()
    {
        for (int j = 0; j < _height; j++)
        {
            for (int i = 0; i < _width; i++)
            {
                if (CheckForDeath(i, j)) continue;
                if(CheckForBirth(i, j)) Add(i,j);
                if(_board[j,i] == 1) Add(i,j);
            }
        }
        FlipBoards();
    }

    public void Add(int x, int y)
    {
        _tempBoard[y, x] = 1;
    }

    public bool CheckForBirth(int x, int y)
    {
        return GetNeighbors(x, y) == 3;
    }

    public bool CheckForDeath(int x, int y)
    {
        var count = GetNeighbors(x,y);
        return count != 3 && count != 2;
    }

    public override string ToString()
    {
        var output = "";
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                output += _board[i, j] == 0 ? " " : "0";
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
        ResetTempBoard();
    }
}