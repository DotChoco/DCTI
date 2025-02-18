using System.Net.NetworkInformation;
using DCTI.Models;
using DCTI.Structs;

namespace DCTI.Components;
public sealed class CheckBox : Component
{
    const char checkboxChecked = '\u2611'; // ☑
    const char checkboxUnchecked = '\u2610'; // ☐

    private bool _checked = false;
    private MText _title = new();
    private char _box;
    
    public bool Checked { get => _checked; set => _checked = value; }
    public MText Title { get => _title; set => _title = value; }
    
    public CheckBox() {}
    public CheckBox(MText title) => _title = title;

    public sealed override Component Render() {
        
        Console.CursorVisible = false;
        SetCursorPosition(transform.position);
        _box = SetAscii();
        Console.Write($"{_box}\t{_title.value}");
        return this;
    }

    public void Toggle() {
        _checked = !_checked;
        _box = SetAscii();
        SetCursorPosition(transform.position);
        Console.Write($"{_box}");
    }

    char SetAscii() => _checked ? checkboxChecked : checkboxUnchecked;
    
    
    
}