﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class DemoShipScene : IScene
    {
        private List<Button> buttons;
        private List<UnholySlider> sliders;
        private SimTestScene world;
        private bool isPressed = false;
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
            world = new SimTestScene();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            if (isPressed)
            {
                foreach (var button in buttons)
                {
                    button.Draw(sb);
                }
                foreach (var slider in sliders)
                {
                    slider.Draw(sb);
                }
            }
            world.Draw(sb);
            //sb.Draw(Resources.Pixel, new Rectangle(100, 100, 100, 100), Color.Blue);
            sb.End();
        }

        public void Update()
        {
            foreach (var button in buttons)
            {
                button.Update();
            }
            foreach (var slider in sliders)
            {
                slider.Update();
            }
            world.Update();
        }
    }
}