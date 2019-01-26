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
        public static Texture2D LeftButton;
        public static Texture2D RightButton;
        public static Texture2D barEmpty;
        public static Texture2D barFull;
        public static Texture2D slider;
        public static Texture2D slider_highlight;
        public static Texture2D weapons;
        public static Texture2D ui_base;





    }
}
