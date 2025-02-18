using System.Text;
using DCTI.Structs;
using DCTI.Structs.Enums;
namespace DCTI.Models;
public abstract class Fields : Component
{
    //Extra Cons 
    private const char SPACE_CHAR = (char)32;
    private const char CARRIAGE_RETURN = '\n';

    
    //Chars to make the Field
    private char _topLeftCorner;
    private char _topRightCorner;
    
    private char _innerLine;
    private char _leftSideLine;
    private char _rightSideLine;
    private char _intersectionLine;
    
    private char _verticalLine;
    private char _verticalBottonLine;
    private char _verticalTopLine;
    
    private char _bottonLeftCorner;
    private char _bottonRightCorner;

    
    
    //Variables
    private StringBuilder _data = new();
    private string[] _lines = Array.Empty<string>();

    protected string _color = Color.DEFAULT_COLOR;
    protected const string DEFAULT_BORDER_COLOR = Color.DEFAULT_COLOR;
    protected FieldStyles Style = FieldStyles.Basic;
    protected StringBuilder SbData { get=> _data; set => _data = value; }
    protected int _width;
    protected int _height;
    protected int _minWidth = 10;
    protected int _minHeight = 1;
    
    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }
    
    
    //Size Methods
    public Component SetSize(int width, int height)
        => SetSize(new(width, height));
    public Component SetSize(Vector2 size) {
        _width = size.x > _minWidth ? size.x : _minWidth;
        _height = size.y > _minHeight ? size.y : _minHeight;
        return this;
    }

    public Vector2 GetSize()  => new (_width, _height);
    
    
    
    
    //Render Methods
    public override Component Render()
    {
        Color.SetTextColor(_color);
        string[] lines = _data.ToString().Split('\n');
        
        for (int i = 0; i < lines.Length; i++) {
            SetCursorPosition(transform.position.x, transform.position.y + i);
            Console.Write(lines[i]);
        }
        
        
        return this;
    }
    public Component SetColor(string hex) {
        Color.SetTextColor(_color);
        Color.SetTextColor(hex);
        return this;
    }
    
    
    
    
    
    
    
    //Style Methods
    protected void MakeMidLine(
        int colSize, int width , 
        int height = 1, bool separator = false
    ) {
        SetStyle();
        char middleChar = separator ? _innerLine : SPACE_CHAR;
        for (int i = 0; i < height; i++)
        {
            //Draw the first item of the line
            _data.Append(DrawFirstItem(middleChar, _leftSideLine, 
                width, separator));
            
            //Draw the mid items of the line
            for (int j = 0; j < colSize; j++) {
                
                //Draw the last item of the line
                if (j == colSize - 1) {
                    _data.Append(DrawLastItem(middleChar, _rightSideLine, 
                        width, separator));
                }
                else {
                    _data.Append(DrawLastItem(middleChar, _intersectionLine, 
                        width, separator));
                }
            }
            _data.Append(CARRIAGE_RETURN);
        }
    }
    
    protected void MakeTopLine(
        int colSize, int width
    ){
        SetStyle();
        
        //left cornner
        _data.Append(_topLeftCorner);
        
        for (int i = 0; i < colSize; i++) {
            _data.Append(new string(_innerLine, width));
            
            if (colSize > 1 && i < colSize - 1)
                _data.Append(_verticalTopLine);
        }
        //right cornner
        _data.Append(_topRightCorner);
        _data.Append(CARRIAGE_RETURN);
        
    }
    
    protected void MakeBottonLine(
        int colSize, int width
    ) {
        SetStyle();
        
        //left cornner
        _data.Append(_bottonLeftCorner);

        for (int i = 0; i < colSize; i++) {
            _data.Append(new string(_innerLine, width));
        
            if (colSize > 1 && i < colSize - 1)
                _data.Append(_verticalBottonLine);
        }
        
        //right cornner
        _data.Append(_bottonRightCorner);
        _data.Append(CARRIAGE_RETURN);
        
    }

    
    string DrawLastItem(char middleChar, char focusChar,
        int width, bool separator = false)
    {
        StringBuilder sb = new();
        sb.Append(new string(middleChar, width));
                    
        if (separator)
            sb.Append(focusChar);
        else
            sb.Append(_verticalLine);
                    
        return sb.ToString();
    }
    char DrawFirstItem(char middleChar, char focusChar,
        int width, bool separator = false)
        => separator ? focusChar: _verticalLine;
    
    
    
    
    
    
    
    //Style Methods
    private void SetStyle()
    {
        if (Style == FieldStyles.Basic)
            SetBasicStyle();
        else if (Style == FieldStyles.Box)
            SetBoxStyle();
        else if (Style == FieldStyles.Rounded)
            SetRoundedStyle();
    }

    private void SetRoundedStyle()
    {
        _topLeftCorner = '\u256d'; //TLC
        _topRightCorner = '\u256e'; //TRC
        
        _innerLine = '\u2500'; // INNER LINE
        
        _leftSideLine = '\u251C';
        _rightSideLine = '\u2524';
        
        _verticalLine = '\u2502'; // VERTICAL BAR
        _verticalTopLine = '\u252C'; // VERTICAL TOP BAR
        _verticalBottonLine = '\u2534'; // VERTICAL BOTTON BAR
        _intersectionLine = '\u253C'; // INTERSECTION LINE
        
        _bottonLeftCorner = '\u2570'; //BLC
        _bottonRightCorner = '\u256f'; //BRC
        
    }
    
    private void SetBoxStyle()
    {
        _topLeftCorner = '\u250C'; //TLC
        _topRightCorner = '\u2510'; //TRC
        
        _innerLine = '\u2500'; // INNER LINE
        
        _leftSideLine = '\u251C';
        _rightSideLine = '\u2524';
        
        _verticalLine = '\u2502'; // VERTICAL BAR
        _verticalTopLine = '\u252C'; // VERTICAL TOP BAR
        _verticalBottonLine = '\u2534'; // VERTICAL BOTTON BAR
        _intersectionLine = '\u253C'; // INTERSECTION LINE
        
        _bottonLeftCorner = '\u2514'; //BLC
        _bottonRightCorner = '\u2518'; //BRC
    }
    
    private void SetBasicStyle()
    {
        _topLeftCorner = '+'; //TLC
        _topRightCorner = '+'; //TRC
        
        _innerLine = '-'; // INNER LINE
        _leftSideLine = '|';
        _rightSideLine = '|';
        
        _verticalLine = '|'; // VERTICAL BAR
        _verticalTopLine = '|'; // VERTICAL TOP BAR
        _verticalBottonLine = '|'; // VERTICAL BOTTON BAR
        _intersectionLine = '|';
        
        _bottonLeftCorner = '+'; //BLC
        _bottonRightCorner = '+'; //BRC
    }
    
    
    
    
    
}