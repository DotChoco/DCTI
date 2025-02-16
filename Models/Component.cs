using DCTI.Interfaces;
using DCTI.Structs;

namespace DCTI.Models;
public abstract class Component
{
    //Variables
    protected Transform transform = new();
    protected Vector2 CursorPosition = new();
    
    
    //Cursor Methods
    protected Vector2 GetCursorPosition() => CursorPosition;
    protected void SetCursorPosition(int x, int y)
        => SetCursorPosition(new Vector2(x, y));
    protected void SetCursorPosition(Vector2 newPosition = new()) {
        CursorPosition = newPosition;
        Cursor.SetCursorPosition(transform.position, CursorPosition, newPosition);
    }


    //Position Methods
    public Component SetPosition(int x, int y) => SetPosition(new Vector2(x, y));
    public Component SetPosition(Vector2 position = default) {
        //If dont have space in the terminal, it make more
        TerminalCofig.ExpandTerminalSize(position);
        
        transform.position = position;
        return this;
    }
    public Vector2 GetPosition() => transform.position;
    
    
    //Render Methods
    public abstract void Render();

}
