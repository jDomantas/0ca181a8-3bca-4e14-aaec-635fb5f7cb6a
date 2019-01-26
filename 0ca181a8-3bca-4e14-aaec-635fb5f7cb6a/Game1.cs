using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public IScene _currentScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            _currentScene = new DemoScene();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Resources.Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Resources.Pixel.SetData(new[] { Color.White });

            const int CircleRadius = 100;
            Resources.Circle = new Texture2D(GraphicsDevice, CircleRadius * 2, CircleRadius * 2);
            var colors = new Color[CircleRadius * CircleRadius * 4];
            for (int x = 0; x < CircleRadius * 2; x++)
                for (int y = 0; y < CircleRadius * 2; y++)
                    colors[x + y * CircleRadius * 2] = (new Vector(x, y) - new Vector(CircleRadius, CircleRadius)).Length < CircleRadius + 0.1
                        ? Color.White
                        : new Color(0, 0, 0, 0);
            Resources.Circle.SetData(colors);

            Resources.FontArial12 = Content.Load<SpriteFont>("font-arial-12");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _currentScene.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentScene.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
