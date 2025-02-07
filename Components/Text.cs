using DCTI.Models;
using DCTI.Structs;

namespace DCTI.Components
{
    public sealed class Text : Component
    {
        private MColor Color { get; set; } = new();
        private MText _text = new();
        public Text(MText text, Transform transform)
        {
            _text = text;
            Transform = transform;

            MColor.SetTextColor(text.color.ToString());
        }

        public override void Render()
        {
            SetCursorPosition(Transform.position);
            Console.Write(_text.value);
        }

    }
}
