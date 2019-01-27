using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    static class Resources
    {
        public static Texture2D Pixel;
        public static Texture2D Circle;
        public static SpriteFont FontArial12;
        public static Texture2D PlayerShip;
        public static Texture2D EnemyShip;
        public static List<Texture2D> Planets;
        public static List<Texture2D> BlueEngine;
        public static List<Texture2D> RedEngine;
        public static void RemovePink(Texture2D tex)
        {
            var data = new Color[tex.Width * tex.Height];
            tex.GetData(data);
            for (int i = 0; i < data.Length; i++)
                if (data[i].R == 255 && data[i].G == 0 && data[i].B == 255)
                    data[i] = new Color(0, 0, 0, 0);
            tex.SetData(data);
        }
        public static Texture2D PlayButton;
        public static Texture2D PlayButtonHover;
        public static Texture2D ReplayButton;
        public static Texture2D ReplayButtonHover;
        public static Texture2D SubmitButton;
        public static Texture2D SubmitButtonHover;
        public static Texture2D Slider;
        public static Texture2D SliderHover;
        public static Texture2D UIBackground;
        public static Texture2D LeftLabel;
        public static Texture2D RightLabel;
        public static Texture2D WeaponsLabel;
        public static Texture2D BarEmpty;
        public static Texture2D BarFull;
        public static Texture2D BarRed;
        public static List<Texture2D> BlueExplosion;
        public static List<Texture2D> RedExplosion;
        public static Texture2D Background;
        public static Texture2D BlueTurnIndicator;
        public static Texture2D RedTurnIndicator;
    }
}
