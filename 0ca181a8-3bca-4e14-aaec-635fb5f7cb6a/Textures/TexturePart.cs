using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures
{
    class TexturePart
    {
        public List<Texture2D> Texture;
        public Vector Offset;
        public Vector Size;
        public Vector TextureOrigin;
        public float Angle;
        public bool Flip;
        public bool Animate;
        public float Transparency = 1;

        public void Draw(SpriteBatch sb, Vector pos, float angle)
        {
            var frame = (int)Math.Floor(Game1.GlobalTimer * 10) % Texture.Count;
            var tex = Texture[frame];
            var eff = Flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            var verticalScale = !Animate ? 1 : 1 + 0.1 * Math.Sin(Game1.GlobalTimer * 80);

            angle += Angle;
            var xAxis = Vector.AtAngle(angle);
            var p = pos + Offset.Translate(xAxis);
            sb.Draw(tex, new Rectangle((int) p.X, (int) p.Y, (int) Size.X, (int)(Size.Y * verticalScale)), null, Color.White*Transparency, angle, new Vector2((float)TextureOrigin.X, (float)TextureOrigin.Y), eff, 0);
        }

        public void Scale(double s)
        {
            Size /= s;
            Offset /= s;
            Transparency /= 3;
        }

        public TexturePart Clone()
        {
            return new TexturePart()
            {
                Texture = Texture,
                Offset = Offset,
                Size = Size,
                TextureOrigin = TextureOrigin,
                Angle = Angle,
                Flip = Flip,
                Animate = Animate,
                Transparency = Transparency
            };
        }
    }
}
