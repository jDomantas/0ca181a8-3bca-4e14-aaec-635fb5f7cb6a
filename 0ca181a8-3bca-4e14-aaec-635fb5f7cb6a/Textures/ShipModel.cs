﻿using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures
{
    class ShipModel
    {
        public int Team;
        public TexturePart Body;
        public TexturePart LeftEngine;
        public TexturePart RightEngine;
        public TexturePart LeftFlame;
        public TexturePart RightFlame;
        public PolygonHitbox HitBox;
        public List<Texture2D> ExplosionAnimation;

        public void Draw(SpriteBatch sb, Vector pos, float angle, bool leftOn, bool rightOn)
        {
            if (rightOn)
                RightFlame?.Draw(sb, pos, angle);
            else
                RightEngine?.Draw(sb, pos, angle);

            if (leftOn)
                LeftFlame?.Draw(sb, pos, angle);
            else
                LeftEngine?.Draw(sb, pos, angle);

            Body?.Draw(sb, pos, angle);
        }

        public void Scale(double s)
        {
            Body.Scale(s);
            LeftEngine.Scale(s);
            RightEngine.Scale(s);
            LeftFlame.Scale(s);
            RightFlame.Scale(s);
        }

        public ShipModel Clone()
        {
            return new ShipModel()
            {
                Team = Team,
                Body = Body.Clone(),
                LeftEngine = LeftEngine.Clone(),
                RightEngine = RightEngine.Clone(),
                LeftFlame = LeftFlame.Clone(),
                RightFlame = RightFlame.Clone(),
                HitBox = HitBox,
                ExplosionAnimation = ExplosionAnimation
            };
        }
    }
}
