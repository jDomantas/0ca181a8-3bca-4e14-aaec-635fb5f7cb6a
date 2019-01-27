using System;
using System.Collections.Generic;
using System.Linq;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI;
using Microsoft.Xna.Framework;
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
        private readonly Texture2D _turnIndicator;
        private ControlPopup _popup;
        private bool _oldPressed;
        private Action<Dictionary<Guid, ShipCommands>> _onSubmit;

        public GameScene(ISceneHost host, World world, PlaybackManager playbackManager, Texture2D turnIndicator, Action<Dictionary<Guid, ShipCommands>> onSubmit)
        {
            _host = host;
            _turnStart = world;
            _onSubmit = onSubmit;
            _playbackManager = playbackManager;
            this._turnIndicator = turnIndicator;
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
                var pos = new Vector(mouse.X * Game1.ScaleHack, mouse.Y * Game1.ScaleHack);
                if (_popup == null)
                {
                    foreach (var ship in _turnStart.Ships)
                    {
                        if ((ship.Position - pos).Length < 30)
                        {
                            var x = (int)(ship.Position.X / Game1.ScaleHack);
                            var y = (int)(ship.Position.Y / Game1.ScaleHack);
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
            _turnStart.Draw(sb);
            sb.Begin();
            sb.Draw(
                Resources.Background,
                new Rectangle(0, 0, 1600, 900),
                new Rectangle(0, 0, Resources.Background.Width, Resources.Background.Width * 9 / 16),
                Color.LightGray);
            DrawPrediction(sb);
            sb.Draw(Game1.WorldRenderTarget, new Rectangle(0, 0, 1600, 900), Color.White);
            _previewButton.Draw(sb);
            _submitButton.Draw(sb);
            if(_playbackManager.Frames.Count > 0)
            {
                _playbackButton.Draw(sb);
            }
            int indicatorWidth = 200;
            int indicatorHeight = indicatorWidth * _turnIndicator.Height / _turnIndicator.Width;
            sb.Draw(
                _turnIndicator,
                new Rectangle(_host.ScreenWidth - indicatorWidth - 10, 10, indicatorWidth, indicatorHeight),
                Color.White
            );
            _popup?.Draw(sb);
            sb.End();
        }

        private void DrawPrediction(SpriteBatch sb)
        {
            var simWorld = _turnStart.Clone();
            double dt = 1 / 60.0;
            if(_popup != null) _commands[_popup.ShipId] = _popup.CurrentCommands();
            foreach (var ship in simWorld.Ships) GetShipCommands(ship);
            var controllers = _commands.ToDictionary(c => c.Key, c => (IShipController)new PlayerShipController(c.Value));

            var predictionPoints = new Dictionary<Guid, List<Vector>>();
            foreach (var ship in simWorld.Ships) predictionPoints[ship.Uid] = new List<Vector>();

            var shipCheckpoints = new List<Ship>();

            var lasers = new List<Tuple<Vector, Vector>>();

            for (double time = 0; time < World.TurnLength; time += dt)
            {
                simWorld.Update(dt, controllers);
                if(time < 1 && time+dt >= 1 || time < 2 && time+dt >= 2 || time < 3 && time+dt >= 3)
                {
                    foreach(var ship in simWorld.Ships)
                    {
                        shipCheckpoints.Add(ship.Clone());
                    }
                }
                foreach (var ship in simWorld.Ships)
                {
                    predictionPoints[ship.Uid].Add(ship.Position/Game1.ScaleHack);
                    if(ship.ShotTimer > 0)
                    {
                        var len = ship.LazerLength();
                        var start = ship.Position;
                        var d = Vector.AtAngle(ship.Angle) * len;
                        var angle = Math.Atan2(d.Y, d.X);
                        var end = d + start;
                        lasers.Add(new Tuple<Vector, Vector>(start/Game1.ScaleHack, end/Game1.ScaleHack));
                    }
                }
            }
            foreach(var entry in predictionPoints)
            {
                var points = entry.Value;
                for(int i = 0; i < points.Count-1; i++)
                {
                    PolygonHitbox.DrawLine(sb, points[i], points[i + 1], Color.White, 2);
                }
                if(simWorld.Ships.All(s => s.Uid != entry.Key))
                {
                    var lastPoint = points[points.Count - 1];
                    sb.Draw(
                        Resources.Pixel,
                        new Rectangle((int)lastPoint.X, (int)lastPoint.Y, 3, 3),
                        Color.Red
                    );
                }
            }
            foreach(var laser in lasers)
            {
                PolygonHitbox.DrawLine(sb, laser.Item1, laser.Item2, Color.Red*0.1f, 2);
            }
            foreach (var ship in shipCheckpoints)
            {
                ship.Scale(Game1.ScaleHack);
                ship.Draw(sb);
            }
        }
    }
}
