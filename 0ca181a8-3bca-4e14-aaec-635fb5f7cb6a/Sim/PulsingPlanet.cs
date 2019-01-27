using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim
{
    class PulsingPlanet : Planet
    {
        public PulsingPlanet(Vector position, double radius) : base(position, radius, 2)
        {
        }

        public override void Draw(SpriteBatch sb)
        {
            var brightness = (float)(0.7 + Math.Sin(Game1.GlobalTimer * 6) * 0.3);

            sb.Draw(
                Resources.PulsingPlanet[0],
                new Rectangle((int)(Position.X - Radius * 1.7), (int)(Position.Y - Radius * 1.7),
                (int)(Radius * 3.4), (int)(Radius * 3.4)),
                Color.White);

            sb.Draw(
                Resources.PulsingPlanet[1],
                new Rectangle((int)(Position.X - Radius * 1.7), (int)(Position.Y - Radius * 1.7),
                (int)(Radius * 3.4), (int)(Radius * 3.4)),
                Color.White * brightness);

            sb.Draw(
                Resources.PulsingPlanet[2],
                new Rectangle((int)(Position.X - Radius * 1.7), (int)(Position.Y - Radius * 1.7),
                (int)(Radius * 3.4), (int)(Radius * 3.4)),
                Color.White);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                sb.Draw(
                    Resources.Circle,
                    new Rectangle((int)(Position.X - Radius), (int)(Position.Y - Radius),
                    (int)(Radius * 2), (int)(Radius * 2)),
                    Color.Red * 0.5f);
            }
        }

        public override Planet Clone()
        {
            return new PulsingPlanet(Position, Radius);
        }
    }
}
