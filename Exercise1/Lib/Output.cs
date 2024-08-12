namespace Exercise1.Lib;

public class ProgramOutput
{
    public static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press enter to continue.");
        Console.ReadLine();
    }

    public static void Header(string header)
    {
        Console.Clear();
        Console.WriteLine("=========== {0} ===========", header.ToUpper());
        Console.WriteLine();
    }

    public static void Operations(List<string> operations)
    {
        Console.WriteLine($"Enter your selection to select below operations: ");
        for (int i = 1; i <= operations.Count; i++)
        {
            Console.WriteLine($"{i}. {operations[i - 1]}.");
        }
        Console.WriteLine();
        Console.Write($"Enter your selection: ");
    }
}