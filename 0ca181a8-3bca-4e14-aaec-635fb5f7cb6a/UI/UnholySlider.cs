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
        private readonly Color ActiveColor = Color.Blue;
        private readonly Color InactiveColor = Color.Red;
        private readonly int BarWidth = 6;

        private Vector2 _position;
        private Vector2 _size;
        private double _maxPercentage;
        private List<Button> _bars;
        private Button _clickedBar;

        public UnholySlider(int x, int y, int w, int h, double maxPercentage)
        {
            _position = new Vector2(x, y);
            _size = new Vector2(w, h);
            _maxPercentage = maxPercentage;
            _bars = new List<Button>();
        }

        public void Update()
        {
            _clickedBar = null;
            foreach(var bar in _bars)
            {
                bar.Update();
            }
            if(_clickedBar != null)
            {
                _bars.Remove(_clickedBar);
            }
            else
            {
                var mouseState = Mouse.GetState();
                var mousePoint = new Point(mouseState.X, mouseState.Y);
                var sliderRect = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y);
                if(sliderRect.Contains(mousePoint) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    // add bar
                    var bar = new Button(mousePoint.X - BarWidth / 2, (int)_position.Y, BarWidth, (int)_size.Y);
                    bar.OnClick += b => _clickedBar = b;
                    _bars.Add(bar);
                }
            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Resources.Pixel, new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y), Color.White);
            foreach(var bar in _bars)
            {
                bar.Draw(sb);
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
