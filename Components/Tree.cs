using DCTI.Models;
using DCTI.Structs;

namespace DCTI.Components;
/// <summary>
/// Tree Item
/// </summary>
// ReSharper disable once InconsistentNaming
public sealed class TItem
{
    public string HexColor { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<TItem> Children { get; set; } = new();
}


public sealed class Tree : Component
{

    public List<TItem> Content { get => _content; set => _content = value; }
    private List<TItem> _content = new();
    int spaces = 1;

    public Tree(List<TItem> items, Vector2 position)
    {
        _content = items;
        transform.position = position;
    }

    public Tree() { }

    public sealed override void Render()
    {
        RenderFathers(_content);
    }

    void RenderFathers(List<TItem> items)
    {
        int posy = transform.position.y;
        SetCursorPosition(transform.position);

        foreach (var item in items)
        {
            Color.SetTextColor(item.HexColor);
            Console.Write($"{item.Content}\n");

            posy = CursorPosition.y + 1;
            SetCursorPosition(new(CursorPosition.x, posy));
            if (item.Children != null && item.Children.Count > 0)
            {
                RenderChildren(item.Children);
                spaces--;
            }
        }
    }

    void RenderChildren(List<TItem> items)
    {
        spaces++;
        int posy = CursorPosition.y;
        SetCursorPosition(CursorPosition);

        foreach (var item in items)
        {
            Color.SetTextColor(item.HexColor);
            if (item.Content != null && item.Content != string.Empty)
            {
                var tabs = new string(' ', spaces);
                var lines = new string('-', (spaces / 2));
                Console.Write($"{tabs}|{lines}{item.Content}\n");
            }

            posy = CursorPosition.y + 1;
            SetCursorPosition(new(CursorPosition.x, posy));
            if (item.Children != null && item.Children.Count > 0)
            {
                RenderChildren(item.Children);
                spaces--;
            }
        }
    }
}