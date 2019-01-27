using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI
{
    class HolySlider
    {
        private const int SelectorSnapDistance = 5;

        private readonly int _x;
        private readonly int _y;
        private readonly int _width;
        private readonly int _height;
        private bool _drawFill;
        private double _maxFill;
        private List<double> _selections;
        private bool _oldMouseDown;

        public List<double> ActivePoints => _selections.ToList();

        public HolySlider(int x, int y, int width, int height, List<double> points)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _selections = points.ToList();
        }

        public HolySlider(int x, int y, int width, int height, List<double> points, double maxFill)
            : this(x, y, width, height, points)
        {
            _drawFill = true;
            _maxFill = maxFill;
        }

        public void Update()
        {
            var mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed && !_oldMouseDown && mouse.Y >= _y && mouse.Y < _y + _height)
            {
                var (distance, index) = ClosestSelector(mouse.X);
                if (distance <= SelectorSnapDistance)
                    _selections.RemoveAt(index);
                else
                {
                    double at = (double)(mouse.X - _x) / _width;
                    if (at >= 0 && at <= 1)
                    {
                        _selections.Add(at);
                        _selections.Sort();
                    }
                }
            }

            _oldMouseDown = mouse.LeftButton == ButtonState.Pressed;
        }

        private (int distance, int index) ClosestSelector(int mouseX)
        {
            int closest = -1, closestDist = int.MaxValue;
            for (int i = 0; i < _selections.Count; i++)
            {
                int x = (int)(_x + _width * _selections[i]);
                int dist = Math.Abs(x - mouseX);
                if (dist < closestDist)
                {
                    closest = i;
                    closestDist = dist;
                }
            }
            return (closestDist, closest);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Resources.BarEmpty, new Rectangle(_x, _y, _width, _height), Color.White);

            if (_drawFill)
            {
                var greenLeft = _maxFill;
                for (int i = 0; i < _selections.Count; i += 2)
                {
                    double len;
                    if (i == _selections.Count - 1)
                        len = 1 - _selections[i];
                    else
                        len = _selections[i + 1] - _selections[i];
                    var greenLen = Math.Min(len, greenLeft);
                    var redLen = Math.Max(0, len - greenLen);
                    greenLeft -= greenLen;
                    var startX = (int)Math.Round(_width * _selections[i]);
                    int green = (int)Math.Round(_width * greenLen);
                    int red = (int)Math.Round(_width * redLen);
                    if (greenLen > 0) DrawPart(sb, Resources.BarFull, startX, green);
                    if (redLen > 0) DrawPart(sb, Resources.BarRed, startX + green, red);
                }
            }

            var mouse = Mouse.GetState();
            var (distance, selector) = ClosestSelector(mouse.X);
            for (int i = 0; i < _selections.Count; i++)
            {
                var tex = i == selector && distance <= SelectorSnapDistance ? Resources.SliderHover : Resources.Slider;
                var x = (int)Math.Round(_x + _width * _selections[i]);
                sb.Draw(
                    tex,
                    new Rectangle(x, _y, tex.Width * _height / tex.Height, _height),
                    null,
                    Color.White,
                    0,
                    new Vector2(tex.Width / 2, 0),
                    SpriteEffects.None,
                    0);
            }
        }

        private void DrawPart(SpriteBatch sb, Texture2D tex, int x, int width)
        {
            sb.Draw(
                tex,
                new Rectangle(_x + x, _y, width, _height),
                new Rectangle(tex.Width * x / _width, 0, width * tex.Width / _width, tex.Height),
                Color.White);
        }
    }
}
