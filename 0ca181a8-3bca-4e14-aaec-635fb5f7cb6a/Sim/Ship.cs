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
        public Rectangle area;
        public Ship(Vector position)
        {
            Position = position;
            Velocity = Vector.Zero;
            Angle = 0;
            RotationSpeed = 0;
        }

        public void Update(World world, double dt)
        {
            AddGravity(world, dt);
            Position += Velocity * dt;

            Angle += RotationSpeed * dt;
            RotationSpeed /= Math.Exp(dt);
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

        public void Draw(SpriteBatch sb)
        {
            const int Size = 30;
            area = new Rectangle((int)Position.X - Size, (int)Position.Y - Size, Size * 2, Size * 2);
            sb.Draw(
                Resources.Pixel,
                new Rectangle((int)Position.X, (int)Position.Y, Size * 2, Size * 2),
                null,
                Color.Blue,
                (float)Angle,
                new Vector2(0.5f, 0.5f),
                SpriteEffects.None,
                0);
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
