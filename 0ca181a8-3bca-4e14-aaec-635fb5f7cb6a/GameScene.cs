using System;
using System.Collections.Generic;
using System.Linq;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class GameScene : IScene
    {
        private readonly ISceneHost _host;
        private readonly World _turnStart;
        private readonly Dictionary<Guid, ShipCommands> _commands;
        private readonly Button _previewButton;
        private ControlPopup _popup;
        private bool _oldPressed;
        
        public GameScene(ISceneHost host, World world)
        {
            _host = host;
            _turnStart = world;

            _commands = new Dictionary<Guid, ShipCommands>();

            _previewButton = new Button(5, 5, 100);
            _previewButton.OnMouseUp += OnPreview;
        }

        private void OnPreview(Button obj)
        {
            var controllers = _commands.ToDictionary(c => c.Key, c => (IShipController)new PlayerShipController(c.Value));
            _host.SetScene(new PreviewScene(_host, this, _turnStart.Clone(), controllers));
        }

        public void Update()
        {
            _popup?.Update();

            var mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && !_oldPressed)
            {
                var pos = new Vector(mouse.X, mouse.Y);
                if (_popup == null)
                {
                    foreach (var ship in _turnStart.Ships)
                    {
                        if ((ship.Position - pos).Length < 30)
                        {
                            var x = (int)ship.Position.X + 50;
                            var y = (int)ship.Position.Y - 35;

                            _popup = new ControlPopup(ship, x, y, 300, 70, GetShipCommands(ship));
                            break;
                        }
                    }
                }
                else if (!_popup.Contains(mouse.X, mouse.Y))
                {
                    _commands[_popup.ShipId] = _popup.CurrentCommands();
                    _popup = null;
                }
            }

            _oldPressed = mouse.LeftButton == ButtonState.Pressed;

            _previewButton.Update();
        }

        private ShipCommands GetShipCommands(Ship ship)
        {
            if (_commands.TryGetValue(ship.Uid, out var commands))
                return commands;

            _commands[ship.Uid] = new ShipCommands(new List<double>(), new List<double>());
            return _commands[ship.Uid];
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            _turnStart.Draw(sb);
            _previewButton.Draw(sb);
            _popup?.Draw(sb);
            sb.End();
        }
    }
}
