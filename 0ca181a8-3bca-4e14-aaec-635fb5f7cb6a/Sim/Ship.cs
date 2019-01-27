using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class Ship
    {
        public Guid Uid { get; }
        public Vector Position { get; private set; }
        public Vector Velocity { get; private set; }
        public double Angle { get; private set; }
        public double RotationSpeed { get; private set; }

        public ShipModel Model { get; }
        public bool Alive { get; private set; }
        private bool _leftRunning, _rightRunning;

        public Ship(Vector position, ShipModel model, Guid? uid = null)
        {
            Alive = true;
            Uid = uid ?? Guid.NewGuid();
            Model = model;
            Position = position;
            Velocity = Vector.Zero;
            Angle = 0;
            RotationSpeed = 0;
        }

        public void Update(World world, double dt, IShipController controller)
        {
            _leftRunning =  _rightRunning = false;

            controller.Update(world, this, dt);
            ApplyInput(controller, dt);

            AddGravity(world, dt);
            Position += Velocity * dt;

            Angle += RotationSpeed * dt;
            RotationSpeed /= Math.Exp(dt * 4);

            foreach (var ship in world.Ships)
                if (!ReferenceEquals(ship, this) && Model.HitBox.IntersectsOther(ship.Model.HitBox, Position, Angle, ship.Position, ship.Angle))
                {
                    Kill();
                    ship.Kill();
                }

            foreach (var planet in world.Planets)
                if (Model.HitBox.IntersectsPlanet(planet, Position, Angle))
                    Kill();
            if (Position.X < 0 || Position.X >= world.ScreenWidth || Position.Y < 0 || Position.Y >= world.ScreenHeight) Kill();
        }

        public void EndTurn()
        {
            _leftRunning = _rightRunning = false;
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

            Velocity += total * 10000000 * dt;
            Velocity /= Math.Exp(dt / 2);
        }

        private void ApplyInput(IShipController controller, double dt)
        {
            const double RotationPower = 10;
            const double EnginePower = 10;

            var forward = Vector.AtAngle(Angle);
            var forwardSpeed = 0.0;

            if (controller.LeftEngineEnabled)
            {
                _rightRunning = true;
                RotationSpeed -= RotationPower * dt;
                forwardSpeed += 0.5;
            }
            if (controller.RightEngineEnabled)
            {
                _leftRunning = true;
                RotationSpeed += RotationPower * dt;
                forwardSpeed += 0.5;
            }

            Velocity += forward * forwardSpeed * EnginePower;
        }

        public void Draw(SpriteBatch sb)
        {
            Model.Draw(sb, Position, (float)Angle, _leftRunning, _rightRunning);

            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                sb.Draw(
                    Resources.Circle,
                    new Rectangle((int)Position.X - 2, (int)Position.Y - 2, 4, 4),
                    Color.White);
                Model.HitBox.DrawDebug(sb, Position, Angle);
            }
        }

        private void Kill()
        {
            Alive = false;
        }

        public Ship Clone()
        {
            return new Ship(Position, Model, Uid)
            {
                Velocity = Velocity,
                Angle = Angle,
                RotationSpeed = RotationSpeed,
                Alive = Alive,
                _leftRunning = _leftRunning,
                _rightRunning = _rightRunning
            };
        }
    }
}
