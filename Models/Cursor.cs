using DCTI.Models;
using DCTI.Structs;

namespace DCTI.Interfaces;
public sealed class Cursor
{
    public static void SetCursorPosition(
        Vector2 selfPosition, 
        Vector2 cursorPosition,
        int x, int y 
    ){
        SetCursorPosition(selfPosition, cursorPosition,new Vector2(x, y));
    }
    
    public static void SetCursorPosition(
        Vector2 selfPosition,
        Vector2 cursorPosition,
        Vector2 newPosition = default 
    ){
        //If dont have space in the terminal, it make more
        TerminalCofig.ExpandTerminalSize(newPosition);

        //Set Horizontal Axis
        if (newPosition.x == 0)
            cursorPosition = new(selfPosition.x, cursorPosition.y);
        else
            cursorPosition = new(newPosition.x, cursorPosition.y);

        //Set Vertical Axis
        if (newPosition.y == 0)
            cursorPosition = new(cursorPosition.x, selfPosition.y);
        else
            cursorPosition = new(cursorPosition.x, newPosition.y);

        //Set New cursorPosition
        Console.SetCursorPosition(cursorPosition.x, cursorPosition.y);
    }
}