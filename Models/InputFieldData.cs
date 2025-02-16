using DCTI.Structs;
using DCTI.Structs.Enums;
using DCTI.Models;

namespace DCTI.Models;

public sealed class InputFieldData
{
    public MText Placeholder;
    public string ContentColor = Color.DEFAULT_COLOR;
    public string ComponentColor = Color.DEFAULT_COLOR;
    public FieldStyles Style = FieldStyles.Basic;
}