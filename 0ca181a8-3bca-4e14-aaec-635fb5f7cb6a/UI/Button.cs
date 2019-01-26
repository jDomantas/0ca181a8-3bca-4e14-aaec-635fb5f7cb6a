using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI
{
    class Button
    {
        private readonly Color DefaultColor = Color.Gray;
        private readonly Color HoverColor = Color.Blue;
        private readonly Color ClickColor = Color.DarkBlue;
        private readonly Color DefaultTextColor = Color.Black;
        private readonly Color HoverTextColor = Color.White;
        private readonly Color ClickTextColor = Color.White;

        public event Action<Button> OnMouseUp;
        public event Action<Button> OnHover;
        public Vector2 Coords { get; private set; }
        public Vector2 Size { get; private set; }
        public string Text { get; private set; }
        public bool IsHovered { get; private set; }
        public bool IsClicked { get; private set; }

        public Button(int x, int y, int w, int h, string text = null)
        {
            Size = new Vector2(w, h);
            Coords = new Vector2(x, y);
            Text = text;
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            var rect = new Rectangle((int)Coords.X, (int)Coords.Y, (int)Size.X, (int)Size.Y);
            if(rect.Contains(mousePoint))
            {
                IsHovered = true;
                OnHover?.Invoke(this);
                if (IsClicked && mouseState.LeftButton == ButtonState.Released && OnMouseUp != null) OnMouseUp(this);
                IsClicked = mouseState.LeftButton == ButtonState.Pressed;
            }
            else
            {
                IsHovered = false;
                IsClicked = false;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            var color = IsHovered ? (IsClicked ? ClickColor : HoverColor) : DefaultColor;
            sb.Draw(Resources.Pixel, new Rectangle((int)Coords.X, (int)Coords.Y, (int)Size.X, (int)Size.Y), color);
            if(Text != null)
            {
                var textColor = IsHovered ? (IsClicked ? ClickTextColor : HoverTextColor) : DefaultTextColor;
                var textSize = Resources.FontArial12.MeasureString(Text);
                sb.DrawString(Resources.FontArial12, Text, Coords+Size/2-textSize/2, textColor);
            }
        }
    }
}
