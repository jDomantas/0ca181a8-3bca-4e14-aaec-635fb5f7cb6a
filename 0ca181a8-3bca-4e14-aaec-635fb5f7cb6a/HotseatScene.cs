using System;
using System.Collections.Generic;
using System.Linq;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;
using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class HotseatScene : IScene
    {
        private readonly ISceneHost _host;
        private readonly World _world;
        private readonly PlaybackManager _playbackManager;

        public HotseatScene(ISceneHost host, World world, PlaybackManager playbackManager)
        {
            _host = host;
            _world = world;
            _playbackManager = playbackManager;
        }

        public void Update()
        {
            _host.SetScene(new GameScene(_host, _world.Clone(), _playbackManager, commands =>
            {
                _host.SetScene(new GameScene(_host, _world.Clone(), _playbackManager, commands2 =>
                {
                    var com = new Dictionary<Guid, IShipController>();
                    foreach (var c in commands)
                    {
                        var ship = _world.Ships.FirstOrDefault(s => s.Uid == c.Key);
                        if (ship != null && ship.Team == 1)
                            com.Add(c.Key, new PlayerShipController(c.Value));
                    }
                    foreach (var c in commands2)
                    {
                        var ship = _world.Ships.FirstOrDefault(s => s.Uid == c.Key);
                        if (ship != null && ship.Team == 2)
                            com.Add(c.Key, new PlayerShipController(c.Value));
                    }

                    _host.SetScene(new SubmitScene(_host, this, _world, com, _playbackManager));
                }));
            }));
        }

        public void Draw(SpriteBatch sb) { }
    }
}
