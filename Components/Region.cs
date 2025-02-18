using DCTI.Models;
using DCTI.Structs;

namespace DCTI.Components;

public sealed class Region: Fields
{
    public string Color = Models.Color.DEFAULT_COLOR;
    public Region(){}

    public sealed override Component Render() {
        SetData();
        base.Render();
        return this;
    }

    public void SetData() {
        MakeTopLine(1,_width);
        MakeMidLine(1,_width, _height,false);
        MakeBottonLine(1,_width);
    }
}