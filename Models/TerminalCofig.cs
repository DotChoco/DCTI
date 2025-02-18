using DCTI.Structs;

namespace DCTI.Models;

public sealed class TerminalCofig
{
    public static void ExpandTerminalSize(Vector2 size)
    {
        //Make a new vertical space in the console
        if (size.y >= Console.WindowHeight) { Console.WindowHeight++; }
        
        //Make a new horizontal space in the console
        if (size.x >= Console.WindowWidth) { Console.WindowWidth++; }
    }
    
    public static void ReduceTerminalSize()
    {
        //Make a new vertical space in the console
        if (Console.WindowHeight > 0) { Console.WindowHeight--; }
        //Make a new horizontal space in the console
        if (Console.WindowWidth > 0) { Console.WindowWidth--; }
    }
}