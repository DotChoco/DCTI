using System.Text;
using DCTI.Models;
using DCTI.Structs;
using DCTI.Structs.Table;

namespace DCTI.Components;


public sealed class Table : Fields
{

    #region Variables

    //Private
    private TbContent _tb;
    private string _textColor = Color.DEFAULT_COLOR;
    private string[] _tbRender = Array.Empty<string>();

    //public
    public string PlaceHolder = string.Empty;

    #endregion


    public Table() =>  _tb = null;
    public Table(TbContent tb) => _tb = tb;

    public void SetData(TbContent tb) {
        _tb = tb;
        Style = _tb.Style;
        SbData.Clear();
    }
    
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
        MakeTopLine(colSize, _tb.FieldsWidth);
        
        //Make the rows
        for (int i = 0; i < rowSize; i++) {
            MakeMidLine(colSize, _tb.FieldsWidth, 
                tableData[i].height);
            
            //Make a separator bettwen Rows
            if (_tb.SeparetedRows && i < rowSize - 1) {
                MakeMidLine(colSize, _tb.FieldsWidth, 1, true);
            }
        }
        
        //Make Botton Separator
        MakeBottonLine(colSize, _tb.FieldsWidth);
    }
    
    
    bool IsHigherThanPreview(ItemData savedItem, ItemData actualItem)
        => actualItem.length > savedItem.length;
    
    public sealed override Component Render()
    {
        if (_tb != null)
        {
            SetData(_tb);
            BorderMapping();
            base.Render();
        }
        return this;
    }

}

