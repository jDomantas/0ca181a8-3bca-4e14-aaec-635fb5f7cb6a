using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures
{
    class ShipModel
    {
        public TexturePart Body;
        public TexturePart LeftEngine;
        public TexturePart RightEngine;
        public TexturePart LeftFlame;
        public TexturePart RightFlame;

        public void Draw(SpriteBatch sb, Vector pos, float angle, bool leftOn, bool rightOn)
        {
            Body.Draw(sb, pos, angle);

            if (rightOn)
                RightFlame.Draw(sb, pos, angle);
            else
                RightEngine.Draw(sb, pos, angle);

            if (leftOn)
                LeftFlame.Draw(sb, pos, angle);
            else
                LeftEngine.Draw(sb, pos, angle);
        }
    }
}
