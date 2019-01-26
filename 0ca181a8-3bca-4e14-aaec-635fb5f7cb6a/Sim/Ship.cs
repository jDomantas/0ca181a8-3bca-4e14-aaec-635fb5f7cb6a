using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class Ship
    {
        public Vector Position { get; private set; }
        public Vector Velocity { get; private set; }
        public double Angle { get; private set; }
        public double RotationSpeed { get; private set; }

        public Ship(Vector position)
        {
            Position = position;
            Velocity = Vector.Zero;
            Angle = 0;
            RotationSpeed = 0;
        }

        public void Update(World world, double dt, IShipController controller)
        {
            controller.Update(world, this, dt);
            ApplyInput(controller, dt);

            AddGravity(world, dt);
            Position += Velocity * dt;

            Angle += RotationSpeed * dt;
            RotationSpeed /= Math.Exp(dt * 4);
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
                RotationSpeed -= RotationPower * dt;
                forwardSpeed += 0.5;
            }
            if (controller.RightEngineEnabled)
            {
                RotationSpeed += RotationPower * dt;
                forwardSpeed += 0.5;
            }

            Velocity += forward * forwardSpeed * EnginePower;
        }

        public void Draw(SpriteBatch sb)
        {
            const int Size = 30;

            sb.Draw(
                Resources.Pixel,
                new Rectangle((int)Position.X, (int)Position.Y, Size * 2, Size * 2),
                null,
                Color.Blue,
                (float)Angle,
                new Vector2(0.5f, 0.5f),
                SpriteEffects.None,
                0);

            var front = Position + Vector.AtAngle(Angle) * 30;
            sb.Draw(
                Resources.Circle,
                new Rectangle((int)front.X - 5, (int)front.Y - 5, 10, 10),
                null,
                Color.DarkBlue);
        }

        public Ship Clone()
        {
            return new Ship(Position)
            {
                Velocity = Velocity,
                Angle = Angle,
                RotationSpeed = RotationSpeed,
            };
        }
    }
}
