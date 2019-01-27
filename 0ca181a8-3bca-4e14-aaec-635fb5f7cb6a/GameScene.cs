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
        private readonly Button _submitButton;
        private readonly Button _playbackButton;
        private readonly PlaybackManager _playbackManager;
        private ControlPopup _popup;
        private bool _oldPressed;
        private Action<Dictionary<Guid, ShipCommands>> _onSubmit;

        public GameScene(ISceneHost host, World world, PlaybackManager playbackManager, Action<Dictionary<Guid, ShipCommands>> onSubmit)
        {
            _host = host;
            _turnStart = world;
            _onSubmit = onSubmit;
            _playbackManager = playbackManager;

            _commands = new Dictionary<Guid, ShipCommands>();

            _submitButton = new Button(Resources.SubmitButton, Resources.SubmitButtonHover, host.ScreenWidth-100-10, host.ScreenHeight - 70, 100);
            _submitButton.OnMouseUp += OnSubmit;

            _previewButton = new Button(Resources.PlayButton, Resources.PlayButtonHover, host.ScreenWidth-100-10-100-10, host.ScreenHeight-70, 100);
            _previewButton.OnMouseUp += OnPreview;

            _playbackButton = new Button(Resources.ReplayButton, Resources.ReplayButtonHover, host.ScreenWidth-100-10-100-10-100-10, host.ScreenHeight - 70, 100);
            _playbackButton.OnMouseUp += OnPlayback;
        }

        private void OnPlayback(Button obj)
        {
            _host.SetScene(new PlaybackScene(_host, this, _playbackManager));
        }

        private void OnPreview(Button obj)
        {
            var controllers = _commands.ToDictionary(c => c.Key, c => (IShipController)new PlayerShipController(c.Value));
            _host.SetScene(new PreviewScene(_host, this, _turnStart.Clone(), controllers));
        }

        private void OnSubmit(Button obj)
        {
            //var controllers = _commands.ToDictionary(c => c.Key, c => (IShipController)new PlayerShipController(c.Value));
            //_host.SetScene(new SubmitScene(_host, this, _turnStart, controllers, _playbackManager));
            //_commands.Clear();
            _onSubmit(_commands);
            // save snapshot
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
                            var x = (int)ship.Position.X;
                            var y = (int)ship.Position.Y;
                            (x, y) = PlacePopup(x, y, 400, 162);

                            _popup = new ControlPopup(ship, x, y, 400, 162, GetShipCommands(ship));
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
            _submitButton.Update();
            _playbackButton.Update();
        }

        private (int, int) PlacePopup(int centerX, int centerY, int width, int height)
        {
            var x = centerX + 50;
            var y = centerY - 20;
            if (x + width + 10 > _host.ScreenWidth)
            {
                y = centerY + 50;
                x = centerX - 20;
            }
            if (y + height + 10 > _host.ScreenHeight)
            {
                y = centerY - 50 - height;
            }
            x = Math.Min(_host.ScreenWidth - width - 10, x);
            return (x, y);
        }

        private ShipCommands GetShipCommands(Ship ship)
        {
            if (_commands.TryGetValue(ship.Uid, out var commands))
                return commands;

            _commands[ship.Uid] = new ShipCommands(new List<double>(), new List<double>(), new List<double>());
            return _commands[ship.Uid];
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            _turnStart.Draw(sb);
            _previewButton.Draw(sb);
            _submitButton.Draw(sb);
            if(_playbackManager.Frames.Count > 0)
            {
                _playbackButton.Draw(sb);
            }
            _popup?.Draw(sb);
            sb.End();
        }
    }
}
