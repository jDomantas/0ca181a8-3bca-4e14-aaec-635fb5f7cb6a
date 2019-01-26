using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class SimTestScene : IScene
    {
        private World _world;
        private Dictionary<Guid, IShipController> _controllers;

        public SimTestScene()
        {
            _world = new World();
            // _world.Planets.Add(new Planet(new Vector(300, 400), 80));
            // _world.Planets.Add(new Planet(new Vector(600, 200), 60));

            _world.Ships.Add(new Ship(new Vector(250, 280), Models.BlueModel));
            _controllers = new Dictionary<Guid, IShipController>
            {
                [_world.Ships[0].Uid] = new KeyboardShipController(),
            };
            //_world.Ships.Add(new Ship(new Vector(550, 380)));
        }

        public void Update()
        {
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
