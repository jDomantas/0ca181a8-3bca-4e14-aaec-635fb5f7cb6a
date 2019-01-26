using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class Planet
    {
        public Vector Position { get; }
        public double Radius { get; }

        public Planet(Vector position, double radius)
        {
            Position = position;
            Radius = radius;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(
                Resources.Circle,
                new Rectangle((int)(Position.X - Radius), (int)(Position.Y - Radius),
                (int)Radius * 2, (int)Radius * 2),
                Color.Red);
        }

        public Planet Clone()
        {
            return new Planet(Position, Radius);
        }
    }
}
