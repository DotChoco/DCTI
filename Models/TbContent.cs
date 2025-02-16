using DCTI.Structs;
using DCTI.Structs.Enums;

namespace DCTI.Models;
public sealed class TbContent
{
    public Align ItemAlign { get; set; } = Align.Center;
    public MText[,] Content { get; set; }
    public string TbColor { get; set; } = "AC90D8";
    public byte FieldsHeight { get; set; } = 1;
    public byte FieldsWidth { get; set; } = 10;
    public bool SeparetedRows { get; set; } = false;
    public TbContent(MText[,] content) => Content = content;
    public FieldStyles Style { get; set; }
    public TbContent() { }

}
