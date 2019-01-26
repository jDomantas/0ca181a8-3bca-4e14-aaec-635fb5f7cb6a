using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class DemoScene : IScene
    {
        public void Update() { }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(Resources.Pixel, new Rectangle(100, 100, 100, 100), Color.Red);
            sb.End();
        }
    }
}