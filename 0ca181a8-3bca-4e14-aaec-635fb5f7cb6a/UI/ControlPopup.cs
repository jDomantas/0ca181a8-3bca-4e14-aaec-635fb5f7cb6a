using System;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI
{
    class ControlPopup
    {
        private const int LabelMargin = 68;
        public Guid ShipId { get; }
        private readonly HolySlider _leftEngineSlider;
        private readonly HolySlider _rightEngineSlider;
        private readonly HolySlider _weaponsSlider;
        private readonly int _x;
        private readonly int _y;
        private readonly int _width;
        private readonly int _height;

        public ControlPopup(Ship ship, int x, int y, int width, int height, ShipCommands startCommands)
        {
            ShipId = ship.Uid;
            _leftEngineSlider = new HolySlider(x + LabelMargin - 5, y + 10, width - 6 - LabelMargin, 35, startCommands.LeftEngineToggles, World.MaxEnginesPerTurn / World.TurnLength);
            _rightEngineSlider = new HolySlider(x + LabelMargin - 5, y + 63, width - 6 - LabelMargin, 35, startCommands.RightEngineToggles, World.MaxEnginesPerTurn / World.TurnLength);
            _weaponsSlider = new HolySlider(x + LabelMargin - 5, y + 116, width - 6 - LabelMargin, 35, startCommands.WeaponShots, 2);
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public ShipCommands CurrentCommands()
        {
            return new ShipCommands(
                _leftEngineSlider.ActivePoints,
                _rightEngineSlider.ActivePoints,
                _weaponsSlider.ActivePoints);
        }

        public void Update()
        {
            _leftEngineSlider.Update();
            _rightEngineSlider.Update();
            _weaponsSlider.Update();
        }

        public bool Contains(int x, int y)
        {
            return x >= _x && y >= _y && x < _x + _width && y < _y + _height;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(
                Resources.UIBackground,
                new Rectangle(_x, _y, _width, _height),
                Color.White);
            sb.Draw(Resources.LeftLabel,
                new Rectangle(_x + 10, _y + 10, 37, 35),
                Color.White);
            sb.Draw(Resources.RightLabel,
                new Rectangle(_x + 10, _y + 63, 37, 35),
                Color.White);
            sb.Draw(Resources.WeaponsLabel,
                new Rectangle(_x + 10, _y + 116, 37, 35),
                Color.White);
            _leftEngineSlider.Draw(sb);
            _rightEngineSlider.Draw(sb);
            _weaponsSlider.Draw(sb);
        }
    }
}
