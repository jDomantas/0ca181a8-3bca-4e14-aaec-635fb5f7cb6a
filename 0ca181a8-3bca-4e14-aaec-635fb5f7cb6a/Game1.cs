﻿using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class Game1 : Game, ISceneHost
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public IScene _currentScene;

        public int ScreenWidth => 1600;
        public int ScreenHeight => 900;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;

            Window.Position = new Point(
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (_graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (_graphics.PreferredBackBufferHeight / 2));
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

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
            Resources.PlayerShip = Content.Load<Texture2D>("good512");
            Resources.EnemyShip = Content.Load<Texture2D>("bad512");
            Resources.Planets = new List<Texture2D>(){
                Content.Load<Texture2D>("Planet1-512"),
                Content.Load<Texture2D>("Planet2-512"),
                Content.Load<Texture2D>("Planet3-512"),
                Content.Load<Texture2D>("Planet4-512"),
                Content.Load<Texture2D>("Planet5-512"),
                Content.Load<Texture2D>("Planet6-512")
            };
            Resources.BlueEngine = new List<Texture2D>(){
                Content.Load<Texture2D>("blueng-512"),
                Content.Load<Texture2D>("blueng1-512"),
                Content.Load<Texture2D>("blueng2-512"),
                Content.Load<Texture2D>("blueng3-512"),
                Content.Load<Texture2D>("blueng4-512")
            };
            Resources.RedEngine = new List<Texture2D>(){
                Content.Load<Texture2D>("redeng-512"),
                Content.Load<Texture2D>("redeng1-512"),
                Content.Load<Texture2D>("redeng2-512"),
                Content.Load<Texture2D>("redeng3-512"),
                Content.Load<Texture2D>("redeng4-512")
            };
            var world = new World();
            world.Ships.Add(new Ship(new Vector(100, 100), PolygonHitbox.DefaultBox));
            world.Ships.Add(new Ship(new Vector(400, 150), PolygonHitbox.DefaultBox));
            world.Ships.Add(new Ship(new Vector(150, 400), PolygonHitbox.DefaultBox));

            world.Planets.Add(new Planet(new Vector(600, 500), 100, 0));
            world.Planets.Add(new Planet(new Vector(900, 800), 50, 1));
            world.Planets.Add(new Planet(new Vector(1100, 100), 50, 5));
            world.Planets.Add(new Planet(new Vector(1900, 450), 500, 2));
            _currentScene = new GameScene(this, world);
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

        public void SetScene(IScene scene)
        {
            _currentScene = scene;
        }
    }
}
