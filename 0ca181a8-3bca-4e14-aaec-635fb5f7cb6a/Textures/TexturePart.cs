using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures
{
    class TexturePart
    {
        public Texture2D Texture;
        public Vector Offset;
        public Vector Size;
        public Vector TextureOrigin;
        public float Angle;

        public void Draw(SpriteBatch sb, Vector pos, float angle)
        {
            angle += Angle;
            var xAxis = Vector.AtAngle(angle);
            var p = pos + Offset.Translate(xAxis);
            sb.Draw(Texture, new Rectangle((int) p.X, (int) p.Y, (int) Size.X, (int) Size.Y), null, Color.White, angle, new Vector2((float)TextureOrigin.X, (float)TextureOrigin.Y), SpriteEffects.None, 0);
        }
    }
}
