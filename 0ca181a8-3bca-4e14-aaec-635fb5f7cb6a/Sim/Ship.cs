﻿using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class Ship
    {
        private const double ShotTime = 0.3;

        public Guid Uid { get; }
        public Vector Position { get; private set; }
        public Vector Velocity { get; private set; }
        public double Angle { get; private set; }
        public double RotationSpeed { get; private set; }

        public ShipModel Model { get; }
        public bool Alive { get; private set; }
        private bool _leftRunning, _rightRunning;
        private double _leftEngineRemaining, _rightEngineRemaining;
        public double ShotTimer { get; private set; }

        public Ship(Vector position, ShipModel model, Guid? uid = null)
        {
            Alive = true;
            Uid = uid ?? Guid.NewGuid();
            Model = model;
            Position = position;
            Velocity = Vector.Zero;
            Angle = MathHelper.PiOver2;
            RotationSpeed = 0;
            _leftEngineRemaining = _rightEngineRemaining = World.MaxEnginesPerTurn;
            ShotTimer = 0;
        }

        public void Update(World world, double dt, IShipController controller)
        {
            _leftRunning =  _rightRunning = false;

            controller.Update(world, this, dt);
            ApplyInput(controller, dt);

            AddGravity(world, dt);
            Position += Velocity * dt;

            Angle += RotationSpeed * dt;
            RotationSpeed /= Math.Exp(dt * 3);

            if (ShotTimer > 0)
            {
                ShotTimer -= dt;
                FireLazer(world);
            }

            foreach (var ship in world.Ships)
                if (!ReferenceEquals(ship, this) && Model.HitBox.IntersectsOther(ship.Model.HitBox, Position, Angle, ship.Position, ship.Angle))
                {
                    Kill();
                    ship.Kill();
                }

            foreach (var planet in world.Planets)
                if (Model.HitBox.IntersectsPlanet(planet, Position, Angle))
                    Kill();
            if (Position.X < 0 || Position.X >= world.ScreenWidth * Game1.ScaleHack || Position.Y < 0 || Position.Y >= world.ScreenHeight * Game1.ScaleHack)
                Kill();
        }

        private void FireLazer(World world)
        {
            var length = LazerLength();
            var angle = Vector.AtAngle(Angle);
            foreach (var ship in world.Ships)
            {
                if (ReferenceEquals(ship, this))
                    continue;
                var dist = ship.Model.HitBox.IntersectRay(ship.Position, ship.Angle, Position, angle);
                if (dist < length)
                    ship.Kill();
            }
        }

        public double LazerLength()
        {
            if (ShotTimer < 0) return 0;
            var maxLength = 300;
            return Math.Min(maxLength, (ShotTime - ShotTimer) * 4000);
        }

        public void EndTurn()
        {
            _leftRunning = _rightRunning = false;
            _leftEngineRemaining = _rightEngineRemaining = World.MaxEnginesPerTurn;
        }

        private void AddGravity(World world, double dt)
        {
            var total = Vector.Zero;

            foreach (var planet in world.Planets)
            {
                var delta = planet.Position - Position;
                if (delta.Length < 20)
                    continue;

                total += delta.Normalized / delta.LengthSquared;
            }

            Velocity += total * 8000000 * dt;
            Velocity /= Math.Exp(dt / 1.4);
        }

        private void ApplyInput(IShipController controller, double dt)
        {
            const double RotationPower = 14;
            const double EnginePower = 7;

            var forward = Vector.AtAngle(Angle);
            var forwardSpeed = 0.0;

            if (controller.LeftEngineEnabled && _leftEngineRemaining > 0)
            {
                _rightRunning = true;
                RotationSpeed -= RotationPower * dt;
                forwardSpeed += 0.5;
                _leftEngineRemaining -= dt;
            }
            if (controller.RightEngineEnabled && _rightEngineRemaining > 0)
            {
                _leftRunning = true;
                RotationSpeed += RotationPower * dt;
                forwardSpeed += 0.5;
                _rightEngineRemaining -= dt;
            }
            if (controller.GunEnabled)
            {
                ShotTimer = ShotTime;
            }

            Velocity += forward * forwardSpeed * EnginePower;
        }

        public void Draw(SpriteBatch sb)
        {
            if (ShotTimer > 0) DrawLazer(sb);
            Model.Draw(sb, Position, (float)Angle, _leftRunning, _rightRunning);

            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                sb.Draw(
                    Resources.Circle,
                    new Rectangle((int)Position.X - 2, (int)Position.Y - 2, 4, 4),
                    Color.Red);
                Model.HitBox.DrawDebug(sb, Position, Angle);
            }
        }

        private void DrawLazer(SpriteBatch sb)
        {
            var len = LazerLength();
            var width = ShotTimer * 12 + 1;
            var start = Position;
            var d = Vector.AtAngle(Angle) * len;
            var angle = Math.Atan2(d.Y, d.X);
            sb.Draw(
                Resources.Pixel,
                new Rectangle((int)Math.Round(start.X), (int)Math.Round(start.Y), (int)Math.Round(d.Length), (int)Math.Ceiling(width)),
                null,
                Color.Red * (float)(ShotTimer + 0.5),
                (float)angle,
                new Vector2(0, 0.5f),
                SpriteEffects.None,
                0);
        }

        private void Kill()
        {
            Alive = false;
        }

        public Ship Clone()
        {
            return new Ship(Position, Model.Clone(), Uid)
            {
                Velocity = Velocity,
                Angle = Angle,
                RotationSpeed = RotationSpeed,
                Alive = Alive,
                _leftRunning = _leftRunning,
                _rightRunning = _rightRunning,
                ShotTimer = ShotTimer,
            };
        }

        public void Scale(double s)
        {
            Model.Scale(s);
            Position /= s;
        }
    }
}
