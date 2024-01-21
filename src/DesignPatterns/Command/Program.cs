var commandList = new List<(ICommand, bool)>();

Console.WriteLine("Use 'Arrows' to add direction commands, 'Backspace' to undo and 'Enter' to run the command list");

while (true)
{
    var key = Console.ReadKey(true).Key;
    if (key == ConsoleKey.UpArrow)
        commandList.Add((new UpCommand(), true));
    else if (key == ConsoleKey.DownArrow)
        commandList.Add((new DownCommand(), true));
     
    else if (key == ConsoleKey.LeftArrow)
        commandList.Add((new LeftCommand(), true));
    else if (key == ConsoleKey.RightArrow)
        commandList.Add((new RightCommand(), true));
    else if (key == ConsoleKey.Backspace)
    {
        var notUndoneCommands = commandList
            .Where(c => c.Item2)
            .SkipLast(commandList.Count(c => !c.Item2));

        if (notUndoneCommands.Any())
            commandList.Add((notUndoneCommands.Last().Item1, false));
    }
    else if (key == ConsoleKey.Enter)
    {
        Console.WriteLine();

        commandList.ForEach(c =>
        {
            if (c.Item2)
                c.Item1.Invoke();
            else
                c.Item1.Undo();
        });

        Console.WriteLine($": {Position.X}, {Position.Y}");

        commandList.Clear();
    }
}

public static class Position
{
    public static int X { get; set; }
    public static int Y { get; set; }
}

public interface ICommand
{
    void Invoke();
    void Undo();
}

public class UpCommand : ICommand
{
    public void Invoke()
    {
        Console.Write("Up ");
        Position.Y++;
    }
    public void Undo()
    {
        Console.Write("Down ");
        Position.Y--;
    }
}
public class DownCommand : ICommand
{
    public void Invoke()
    {
        Console.Write("Down ");
        Position.Y--;
    }
    public void Undo()
    {
        Console.Write("Up ");
        Position.Y++;
    }
}
public class LeftCommand : ICommand
{
    public void Invoke()
    {
        Console.Write("Left ");
        Position.X--;
    }
    public void Undo()
    {
        Console.Write("Right ");
        Position.X++;
    }
}
public class RightCommand : ICommand
{
    public void Invoke()
    {
        Console.Write("Right ");
        Position.X++;
    }
    public void Undo()
    {
        Console.Write("Left ");
        Position.X--;
    }
}
