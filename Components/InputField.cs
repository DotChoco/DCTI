using System.Text;
using DCTI.Models;
using DCTI.Structs.Enums;
using DCTI.Models;

namespace DCTI.Components;
public sealed class InputField : MFields
{

    #region Variables

    //CONS
    const string DEFAULT_PH_COLOR = "A2B9C1";

    //Private
    string _content = string.Empty;
    InputFieldData _data;

    //Encapsulation
    public InputFieldData Data { get => _data; set => _data = value; }
    public string Content => _content; 
    
    #endregion

    public InputField() => _data = null;
    
    public InputField(InputFieldData data) => _data = data;

    void SetData(InputFieldData data) {
        
        //Set Colors for the Placeholder
        _data.Placeholder.color = data.Placeholder.color == string.Empty ? DEFAULT_PH_COLOR : data.Placeholder.color;

        //Set Colors that applied to Borders
        _data.ContentColor = data.ContentColor == string.Empty ? Color.DEFAULT_COLOR : data.ContentColor;

        //Set Colors that applied to Text
        _data.ComponentColor = data.ComponentColor == string.Empty ? DEFAULT_BORDER_COLOR : data.ComponentColor;
        
        //Set Border Style
        Style = data.Style;
        
        SetCursorPosition(new());
    }
    
    
    public sealed override void Render()
    {
        if (_data != null)
        {
            SetData(_data);
            RenderBorders();
            RenderPlaceholder();
        }
    }


    private void RenderBorders()
    {
        Color.SetTextColor(DEFAULT_BORDER_COLOR);
        SetCursorPosition(transform.position);

        string[] data = {
            MakeTopLine(1, _width),
            MakeMidLine(1, _width, _height),
            MakeBottonLine(1, _width)
        };
       
        
        for (int i = 0; i < data.Length; i++) {
            SetCursorPosition(transform.position.x, transform.position.y + i);
            Console.Write(data[i]);
        }
    }


    private void RenderPlaceholder()
    {
        Color.SetTextColor(_data.Placeholder.color);
        SetCursorPosition(transform.position.x + 1, transform.position.y + 1);
        Console.Write(_data.Placeholder.value);
    }
    


    public void ReadInput()
    {
        Console.CursorVisible = false;
        Color.SetTextColor(_data.ContentColor);
        
        // Use the StringBuilder to make the input result
        StringBuilder inputData = new();
        ConsoleKeyInfo keyPress;
        SetCursorPosition(transform.position.x + 1, transform.position.y + 1);
       
        
        while (true) {
            // Read key pressed
            keyPress = Console.ReadKey(intercept: true);
            
            // Keep data in the field and escape
            if (keyPress.Key == ConsoleKey.Enter)
            {
                break;
            }
            // Delete data from the input and escape
            if (keyPress.Key == ConsoleKey.Escape)
            {
                inputData.Clear();
                Console.WriteLine(); // Nueva línea
                break;
            }
            // Delete char per char in the input
            if (keyPress.Key == ConsoleKey.Backspace)
            {
                if (inputData.Length >= 1)
                {
                    if (Console.GetCursorPosition().Left > transform.position.x + 1)
                    {
                        inputData.Length--;
                        Console.Write("\b \b");
                    }
                    //ResetPosition
                    if (inputData.Length == 0)
                    {
                        SetCursorPosition(transform.position.x + 1, transform.position.y + 1);
                        RenderPlaceholder();
                        Color.SetTextColor(_data.ContentColor);
                    } 
                }
            }
            //Save and print the character 
            else
            {
                if (Console.GetCursorPosition().Left <= (CursorPosition.x + _width) - 1)
                {
                    //Delete the PlaceHolder and write the char that you press
                    if (inputData.Length == 0) {
                        SetCursorPosition(transform.position.x + 1, transform.position.y + 1);
                        Console.Write(new string(' ', _data.Placeholder.value.Length));
                        SetCursorPosition(transform.position.x + 1, transform.position.y + 1);
                    }
                    inputData.Append(keyPress.KeyChar);
                    Console.Write(keyPress.KeyChar);
                }
            }
        }

        Color.ResetTextColor();
        _content = inputData.ToString();
    }

}

