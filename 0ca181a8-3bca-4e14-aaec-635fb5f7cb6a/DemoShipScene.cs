using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using Microsoft.Xna.Framework.Input;
namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class DemoShipScene : IScene
    {
        private List<Button> buttons;
        private List<UnholySlider> sliders;
        private World world;
        private int pressedOn = -1;
        public DemoShipScene()
        {
            buttons = new List<Button>()
            {
                new Button(50, 200, 100, 50, "Test1"),
                new Button(200, 200, 100, 50, "Test2"),
                new Button(350, 200, 100, 50, "Test3"),
            };
            sliders = new List<UnholySlider>()
            {
                new UnholySlider(50, 50, 300, 20, 0.5),
                new UnholySlider(50, 100, 300, 20, 0.5)
            };
            world = new World();
            world.Ships.Add(new Ship(new Vector(200, 150)));
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            if (pressedOn > -1)
            {
                foreach (var button in buttons)
                {
                    button.Draw(sb);
                }
                foreach (var slider in sliders)
                {
                    slider.Draw(sb);
                }
                sb.DrawString(Resources.FontArial12, pressedOn.ToString(), new Vector2(210, 110), Color.White);
            }
            world.Draw(sb);
            //sb.Draw(Resources.Pixel, new Rectangle(100, 100, 100, 100), Color.Blue);
            sb.End();
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            foreach (var button in buttons)
            {
                button.Update();
            }
            foreach (var slider in sliders)
            {
                slider.Update();
            }
            world.Update(1 / 120.0);

            for (int i = 0; i < world.Ships.Count; i++)
            {
                
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (world.Ships[i].area.Contains(mouseState.X, mouseState.Y))
                    {
                            pressedOn = i;
                    }
                    else
                    {
                            pressedOn = -1;
                    }
            }
        }
    }
}

