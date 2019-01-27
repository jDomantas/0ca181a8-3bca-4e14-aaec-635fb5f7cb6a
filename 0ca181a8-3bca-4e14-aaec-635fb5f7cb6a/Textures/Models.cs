using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures
{
    class Models
    {
        public static ShipModel BlueModel => new ShipModel
        {
            Team = 1,
            Body = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.PlayerShip },
                Offset = Vector.Zero,
                Size = new Vector(50, 50) * 1.3,
                TextureOrigin = new Vector(246, 205),
                Angle = MathHelper.PiOver2,
            },
            RightEngine = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.BlueEngine[0] },
                Offset = Vector.UnitX * 10 + Vector.UnitY * 10,
                Size = new Vector(2106, 2891) / 80,
                TextureOrigin = new Vector(175, 50),
                Angle = MathHelper.PiOver2,
            },
            RightFlame = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.BlueEngine[1], Resources.BlueEngine[2], Resources.BlueEngine[3], Resources.BlueEngine[4] },
                Offset = Vector.UnitX * 10 + Vector.UnitY * 10,
                Size = new Vector(2106, 2891) / 80,
                TextureOrigin = new Vector(175, 50),
                Angle = MathHelper.PiOver2,
                Animate = true,
            },
            LeftEngine = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.BlueEngine[0] },
                Offset = Vector.UnitX * -10 + Vector.UnitY * 10,
                Size = new Vector(2106, 2891) / 80,
                TextureOrigin = new Vector(175, 50),
                Angle = MathHelper.PiOver2,
                Flip = true,
            },
            LeftFlame = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.BlueEngine[1], Resources.BlueEngine[2], Resources.BlueEngine[3], Resources.BlueEngine[4] },
                Offset = Vector.UnitX * -10 + Vector.UnitY * 10,
                Size = new Vector(2106, 2891) / 80,
                TextureOrigin = new Vector(175, 50),
                Angle = MathHelper.PiOver2,
                Flip = true,
                Animate = true,
            },
            HitBox = PolygonHitbox.PlayerBox,
            ExplosionAnimation = Resources.BlueExplosion
        };

        public static ShipModel RedModel => new ShipModel
        {
            Team = 2,
            Body = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.EnemyShip },
                Offset = Vector.Zero,
                Size = new Vector(50, 50) * 1.3,
                TextureOrigin = new Vector(255, 250),
                Angle = MathHelper.PiOver2,
            },
            RightEngine = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.RedEngine[0] },
                Offset = Vector.UnitX * 10 + Vector.UnitY * 5,
                Size = new Vector(2106, 2891) / 80,
                TextureOrigin = new Vector(175, 50),
                Angle = MathHelper.PiOver2,
            },
            RightFlame = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.RedEngine[1], Resources.RedEngine[2], Resources.RedEngine[3], Resources.RedEngine[4] },
                Offset = Vector.UnitX * 10 + Vector.UnitY * 5,
                Size = new Vector(2106, 2891) / 80,
                TextureOrigin = new Vector(175, 50),
                Angle = MathHelper.PiOver2,
                Animate = true,
            },
            LeftEngine = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.RedEngine[0] },
                Offset = Vector.UnitX * -10 + Vector.UnitY * 5,
                Size = new Vector(2106, 2891) / 80,
                TextureOrigin = new Vector(175, 50),
                Angle = MathHelper.PiOver2,
                Flip = true,
            },
            LeftFlame = new TexturePart
            {
                Texture = new List<Texture2D> { Resources.RedEngine[1], Resources.RedEngine[2], Resources.RedEngine[3], Resources.RedEngine[4] },
                Offset = Vector.UnitX * -10 + Vector.UnitY * 5,
                Size = new Vector(2106, 2891) / 80,
                TextureOrigin = new Vector(175, 50),
                Angle = MathHelper.PiOver2,
                Flip = true,
                Animate = true,
            },
            HitBox = PolygonHitbox.EnemyBox,
            ExplosionAnimation = Resources.RedExplosion
        };
    }
}
