using System;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI
{
    class ControlPopup
    {
        public Guid ShipId { get; }
        private readonly UnholySlider _leftEngineSlider;
        private readonly UnholySlider _rightEngineSlider;
        private readonly int _x;
        private readonly int _y;
        private readonly int _width;
        private readonly int _height;

        public ControlPopup(Ship ship, int x, int y, int width, int height, ShipCommands startCommands)
        {
            ShipId = ship.Uid;
            _leftEngineSlider = new UnholySlider(x + 10, y + 10, width - 20, 20, 0.5, startCommands.LeftEngineToggles);
            _rightEngineSlider = new UnholySlider(x + 10, y + 40, width - 20, 20, 0.5, startCommands.RightEngineToggles);
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public ShipCommands CurrentCommands()
        {
            return new ShipCommands(
                _leftEngineSlider.ActivePoints,
                _rightEngineSlider.ActivePoints);
        }

        public void Update()
        {
            _leftEngineSlider.Update();
            _rightEngineSlider.Update();
        }

        public bool Contains(int x, int y)
        {
            return x >= _x && y >= _y && x < _x + _width && y < _y + _height;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(
                Resources.Pixel,
                new Rectangle(_x, _y, _width, _height),
                Color.Gray);

            _leftEngineSlider.Draw(sb);
            _rightEngineSlider.Draw(sb);
        }
    }
}
