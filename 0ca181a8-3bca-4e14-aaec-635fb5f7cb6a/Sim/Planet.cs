using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class Planet
    {
        public Vector Position { get; }
        public double Radius { get; }
        public int planetIndex;
        public Planet(Vector position, double radius, int planetIndex = 2 )
        {
            Position = position;
            Radius = radius;
            this.planetIndex = planetIndex;
        }

        public void Draw(SpriteBatch sb)
        {
            
            sb.Draw(
                Resources.Planets[planetIndex],
                new Rectangle((int)(Position.X - Radius*1.7), (int)(Position.Y - Radius*1.7),
                (int)(Radius * 3.4), (int)(Radius * 3.4)),
                Color.White);
            
        }

        public Planet Clone()
        {
            return new Planet(Position, Radius);
        }
    }
}
