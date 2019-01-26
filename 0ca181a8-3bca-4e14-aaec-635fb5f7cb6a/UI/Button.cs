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
        private Vector2 _coords;
        private Vector2 _size;
        private string _text;
        private bool _isHovered;
        private bool _isClicked;

        public Button(int x, int y, int w, int h, string text)
        {
            _size = new Vector2(w, h);
            _coords = new Vector2(x, y);
            _text = text;
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            var rect = new Rectangle((int)_coords.X, (int)_coords.Y, (int)_size.X, (int)_size.Y);
            if(rect.Contains(mousePoint))
            {
                _isHovered = true;
                _isClicked = mouseState.LeftButton == ButtonState.Pressed;
            }
            else
            {
                _isHovered = false;
                _isClicked = false;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            var color = _isHovered ? (_isClicked ? Color.DarkBlue : Color.Blue) : Color.White;
            sb.Draw(Resources.Pixel, new Rectangle((int)_coords.X, (int)_coords.Y, (int)_size.X, (int)_size.Y), color);
            
        }
    }
}
