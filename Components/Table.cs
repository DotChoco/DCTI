using System.Text;
using DCTI.Models;
using DCTI.Structs;
using DCTI.Structs.Table;

namespace DCTI.Components;


public sealed class Table : MFields
{

    #region Variables

    //Private
    private StringBuilder _sb = new();
    private TbContent _tb;
    private string _textColor = Color.DEFAULT_COLOR;
    private string[] _tbRender = Array.Empty<string>();

    //public
    public string PlaceHolder = string.Empty;

    #endregion


    public Table() =>  _tb = null;
    public Table(TbContent tb) => _tb = tb;

    
    private void BorderMapping() {
        int rowSize = _tb.Content.GetLength(0);
        int colSize = _tb.Content.GetLength(1); 
        int indexTbData = 0;
        
        ItemData[] tableData = new ItemData[rowSize];
        ItemData actualItem = new(){ column = 0, row = 0 , length = 0 };
        
        //Get the Longer Item Length by each Row
        for (int i = 0; i < _tb.Content.GetLength(0); i++) {
            for (int j = 0; j < _tb.Content.GetLength(1); j++) {
                actualItem = new() {
                    length = _tb.Content[i, j].value.Length,
                    row = i,
                    column = j,
                    height = 1
                };
                if (IsHigherThanPreview(tableData[indexTbData], actualItem)) {
                    if (actualItem.length > _tb.FieldsWidth){ actualItem.height++; }

                    tableData[indexTbData] = actualItem;
                }
            }
            indexTbData++;
        }
        
        //Make TopSeparator
        _sb.Append(MakeTopLine(colSize, _tb.FieldsWidth));
        
        
        //Make the rows
        for (int i = 0; i < rowSize; i++) {
            _sb.Append(MakeMidLine(colSize, _tb.FieldsWidth, 
                tableData[i].height));
            
            //Make a separator bettwen Rows
            if (_tb.SeparetedRows && i < rowSize - 1) {
                _sb.Append(MakeMidLine(colSize, _tb.FieldsWidth, 1, true));
            }
        }
        
        
        //Make Botton Separator
        _sb.Append(MakeBottonLine(colSize, _tb.FieldsWidth));
        
        _tbRender = _sb.ToString().Split('\n');
    }
    
    
    bool IsHigherThanPreview(ItemData savedItem, ItemData actualItem)
        => actualItem.length > savedItem.length;
    
    public sealed override void Render()
    {
        if (_tb != null) {
            Style = _tb.Style;
            BorderMapping();
            RenderTable();
        }
    }


    private void RenderTable()
    {
        Color.SetTextColor(DEFAULT_BORDER_COLOR);
        for (int i = 0; i < _tbRender.Length; i++)
        {
            SetCursorPosition(transform.position.x, transform.position.y + i);
            Console.Write(_tbRender[i]);
        }
    }

   
}

