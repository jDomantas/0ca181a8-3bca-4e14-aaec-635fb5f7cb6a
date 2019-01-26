using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public void Update(double dt)
        {
            Position += Velocity;

            Angle += RotationSpeed * dt;
            Position += Velocity * dt;
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
