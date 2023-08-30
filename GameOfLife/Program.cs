// See https://aka.ms/new-console-template for more information

var validNumber = false;
Console.Write("How many evolutions: ");
while (!validNumber)
{
    var evolutionsString = Console.ReadLine();
    try
    {
        var evolutions = int.Parse(evolutionsString!);
        validNumber = true;
        Console.WriteLine($"We will evolve {evolutions} times.");
        var game = new GameOfLife.GameOfLife();
        game.Run(evolutions);
    }
    catch(Exception e)
    {
        Console.Write("Please enter a valid number: ");
    }
}