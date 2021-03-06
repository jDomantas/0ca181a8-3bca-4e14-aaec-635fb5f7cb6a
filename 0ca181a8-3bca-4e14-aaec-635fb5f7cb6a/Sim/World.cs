﻿using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class World
    {
        public const double TurnLength = 3;
        public const double MaxEnginesPerTurn = 1.2;
        public const int MaxShotsPerTurn = 1;

        public List<Planet> Planets { get; private set; } = new List<Planet>();
        public List<Ship> Ships { get; private set; } = new List<Ship>();
        public List<Particle> Particles { get; private set; } = new List<Particle>();
        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        public World(int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public World Clone()
        {
            return new World(ScreenWidth, ScreenHeight)
            {
                Planets = Planets.Select(p => p.Clone()).ToList(),
                Ships = Ships.Select(s => s.Clone()).ToList(),
                Particles = Particles.Select(p => p.Clone()).ToList()
            };
        }

        public void Update(double dt, Dictionary<Guid, IShipController> controllers)
        {
            foreach (var ship in Ships)
            {
                if (!controllers.TryGetValue(ship.Uid, out var contr))
                    contr = new EmptyShipController();
                ship.Update(this, dt, contr);
            }

            for (int i = Ships.Count - 1; i >= 0; i--)
            {
                if (!Ships[i].Alive)
                {
                    Particles.Add(new Particle(Ships[i].Position, Ships[i].Model.ExplosionAnimation));
                    Ships[i] = Ships[Ships.Count - 1];
                    Ships.RemoveAt(Ships.Count - 1);
                }
            }

            for(int i = Particles.Count-1; i >= 0; i--)
            {
                Particles[i].Update();
                if(Particles[i].IsFinished)
                {
                    Particles[i] = Particles[Particles.Count - 1];
                    Particles.RemoveAt(Particles.Count - 1);
                }
            }
        }

        public void EndTurn()
        {
            foreach (var ship in Ships)
                ship.EndTurn();
        }

        public void Draw(SpriteBatch sb)
        {
            Game1.GlobalGraphicsDevice.SetRenderTarget(Game1.WorldRenderTarget);
            Game1.GlobalGraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(0, 0, 0, 0));
            sb.Begin();

            foreach (var planet in Planets)
                planet.Draw(sb);

            foreach (var ship in Ships)
                ship.Draw(sb);

            foreach (var particle in Particles)
                particle.Draw(sb);

            sb.End();

            Game1.GlobalGraphicsDevice.SetRenderTarget(null);
        }
    }
}
