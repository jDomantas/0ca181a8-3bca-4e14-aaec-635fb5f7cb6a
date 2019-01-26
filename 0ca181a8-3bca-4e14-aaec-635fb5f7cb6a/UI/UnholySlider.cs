﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI
{
    class UnholySlider
    {
        private readonly Color DefaultColor = Color.White;
        private readonly Color HoverColor = Color.LightGray;
        private readonly Color ActiveColor = new Color(Color.Green, 0.5f);
        private readonly Color InactiveColor = new Color(Color.Red, 0.5f);
        private readonly int BarWidth = 6;

        private Vector2 _position;
        private Vector2 _size;
        private double _maxPercentage;
        private List<Button> _bars;
        private Button _clickedBar;
        private Button _hoveredBar;
        private ButtonState _prevState;
        private bool _isSliderHovered;

        public UnholySlider(int x, int y, int w, int h, double maxPercentage)
        {
            _position = new Vector2(x, y);
            _size = new Vector2(w, h);
            _maxPercentage = maxPercentage;
            _bars = new List<Button>();
            _prevState = ButtonState.Released;
        }

        public void Update()
        {
            _clickedBar = null;
            _hoveredBar = null;
            foreach(var bar in _bars)
            {
                bar.Update();
            }
            if(_clickedBar != null)
            {
                _bars.Remove(_clickedBar);
            }
            else if(_hoveredBar == null)
            {
                var mouseState = Mouse.GetState();
                var mousePoint = new Point(mouseState.X, mouseState.Y);
                var sliderRect = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y);
                _isSliderHovered = sliderRect.Contains(mousePoint);
                if(sliderRect.Contains(mousePoint) && _prevState == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    // add bar
                    var bar = new Button(mousePoint.X - BarWidth / 2, (int)_position.Y, BarWidth, (int)_size.Y);
                    bar.OnMouseUp += b => _clickedBar = b;
                    bar.OnHover += b => _hoveredBar = b;
                    _bars.Add(bar);
                    _bars = _bars.OrderBy(x => x.Coords.X).ToList();
                }
                _prevState = mouseState.LeftButton;
            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Resources.Pixel, new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y), Color.White);
            foreach(var bar in _bars)
            {
                bar.Draw(sb);
            }
            _drawActivity(sb);
            if(_hoveredBar == null && _isSliderHovered)
            {
                var mouseState = Mouse.GetState();
                var mousePoint = new Point(mouseState.X, mouseState.Y);
                sb.Draw(Resources.Pixel, new Rectangle(mousePoint.X - BarWidth / 2, (int)_position.Y, BarWidth, (int)_size.Y), HoverColor);
            }
        }

        private void _drawActivity(SpriteBatch sb)
        {
            int availableLen = (int)(_maxPercentage * _size.X);
            for(int i = 0; i < _bars.Count; i+=2)
            {
                int len;
                if (i == _bars.Count-1)
                {
                    len = (int)(_position.X + _size.X - (_bars[i].Coords.X+BarWidth/2));
                }
                else
                {
                    len = (int)(_bars[i + 1].Coords.X - _bars[i].Coords.X);
                }
                int greenLen = Math.Min(len, availableLen);
                int redLen = Math.Max(0, len - greenLen);
                availableLen -= greenLen;
                sb.Draw(Resources.Pixel, new Rectangle((int)(_bars[i].Coords.X + BarWidth / 2), (int)_position.Y, greenLen, (int)_size.Y), ActiveColor);
                sb.Draw(Resources.Pixel, new Rectangle((int)(_bars[i].Coords.X + BarWidth / 2 + greenLen), (int)_position.Y, redLen, (int)_size.Y), InactiveColor);
            }
        }

        private bool _updateBars(out Button pressedBar)
        {
            pressedBar = null;
            foreach (var bar in _bars)
            {
                bar.Update();
                if(bar.IsClicked)
                {
                    pressedBar = bar;
                }
            }
            return pressedBar != null;
        }
    }
}