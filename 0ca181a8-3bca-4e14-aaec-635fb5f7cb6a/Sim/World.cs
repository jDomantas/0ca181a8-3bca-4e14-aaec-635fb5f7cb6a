﻿using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class World
    {
        public List<Planet> Planets { get; private set; } = new List<Planet>();
        public List<Ship> Ships { get; private set; } = new List<Ship>();

        public World Clone()
        {
            return new World()
            {
                Planets = Planets.Select(p => p.Clone()).ToList(),
                Ships = Ships.Select(s => s.Clone()).ToList(),
            };
        }

        public void Update(double dt)
        {
            foreach (var ship in Ships)
                ship.Update(dt);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (var planet in Planets)
                planet.Draw(sb);

            foreach (var ship in Ships)
                ship.Draw(sb);
        }
    }
}
