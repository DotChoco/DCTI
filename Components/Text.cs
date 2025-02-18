using System.Diagnostics;
using DCTI.Models;
using DCTI.Structs;

namespace DCTI.Components;
public sealed class Text : Component
{
    private MText _text = new();
    public Text(MText text) {
        _text = text;
        Color.SetTextColor(text.color.ToString());
    }
    
    public Text() { }

    public sealed override Component Render()
    {
        SetCursorPosition(transform.position);
        Console.Write(_text.value);
        return this;
    }

}
