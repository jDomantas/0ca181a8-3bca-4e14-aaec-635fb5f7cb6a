﻿using System;
using System.Collections.Generic;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;
using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class PreviewScene : IScene
    {
        protected readonly ISceneHost _host;
        protected readonly IScene _previousScene;
        protected readonly World _world;
        protected readonly Dictionary<Guid, IShipController> _controllers;
        protected double _timePassed;

        public PreviewScene(ISceneHost host, IScene previousScene, World world, Dictionary<Guid, IShipController> controllers)
        {
            _host = host;
            _previousScene = previousScene;
            _world = world;
            _controllers = controllers;
            _timePassed = 0;
        }

        public virtual void Update()
        {
            if (_timePassed >= World.TurnLength)
            {
                _host.SetScene(_previousScene);
                return;
            }

            _timePassed += 1 / 60.0;
            _world.Update(1 / 60.0, _controllers);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            _world.Draw(sb);
            sb.End();
        }
    }
}
