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
        private List<Button> buttons;
        private List<UnholySlider> sliders;

        public DemoUIScene()
        {
            buttons = new List<Button>()
            {
                new Button(50, 200, 100),
                new Button(200, 200, 100),
                new Button(350, 200, 100),
            };
            sliders = new List<UnholySlider>()
            {
                new UnholySlider(50, 50, 300, 20, 0.5, new List<double>(){ 0.1, 0.3, 0.5}),
                new UnholySlider(50, 100, 300, 20, 0.2)
            };
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            foreach(var button in buttons)
            {
                button.Draw(sb);
            }
            foreach(var slider in sliders)
            {
                slider.Draw(sb);
            }
            //sb.Draw(Resources.Pixel, new Rectangle(100, 100, 100, 100), Color.Blue);
            sb.End();
        }

        public void Update()
        {
            foreach (var button in buttons)
            {
                button.Update();
            }
            foreach(var slider in sliders)
            {
                slider.Update();
            }
        }
    }
}
