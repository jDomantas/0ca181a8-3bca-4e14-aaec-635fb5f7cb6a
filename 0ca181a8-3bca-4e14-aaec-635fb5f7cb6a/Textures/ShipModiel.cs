using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures
{
    class ShipModiel
    {
        public ParšelTexture Body;
        public ParšelTexture LeftEngine;
        public ParšelTexture RightEngine;
        public ParšelTexture LeftFlame;
        public ParšelTexture RightFlame;
        public Vector Size;

        public void Draw(SpriteBatch sb, Vector pos, float angel, bool leftOn, bool rightOn)
        {
            Body.Draw(sb, pos, angel);
            LeftEngine.Draw(sb, pos, angel);
            RightEngine.Draw(sb, pos, angel);
            if(rightOn)
                RightFlame.Draw(sb, pos, angel);
            if(leftOn)
                LeftFlame.Draw(sb, pos, angel);
        }
    }
}
