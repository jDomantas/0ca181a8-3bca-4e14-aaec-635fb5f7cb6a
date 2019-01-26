using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures
{
    class ParšelTexture
    {
        public Texture2D Texture;
        public Vector Offset;
        public Vector Size;
        public Vector RotationAxis;
        public float Angel;

        public void Draw(SpriteBatch sb, Vector pos, float angelOffset)
        {
            var angle = angelOffset + Angel;
            var xAxis = Vector.AtAngle(angelOffset);
            Vector daRealPos = pos + (pos + Offset).Translate(xAxis);
            
            sb.Draw(Texture, new Rectangle((int) daRealPos.X, (int) daRealPos.Y, (int) Size.X, (int) Size.Y), null, Color.White, angelOffset + Angel, new Vector2((float) RotationAxis.X, (float) RotationAxis.Y), SpriteEffects.None, 0);
        }
    }
}
