using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class DemoUIScene : IScene
    {
        private Button butt;

        public DemoUIScene()
        {
            butt = new Button(100, 200, 100, 50, "Test");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            butt.Draw(sb);
            //sb.Draw(Resources.Pixel, new Rectangle(100, 100, 100, 100), Color.Blue);
            sb.End();
        }

        public void Update()
        {
            butt.Update();
        }
    }
}
