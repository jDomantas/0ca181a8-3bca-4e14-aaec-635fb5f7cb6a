﻿using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Sim;
using _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    class Game1 : Game, ISceneHost
    {
        public static double GlobalTimer = 0;

        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public IScene _currentScene;

        public int ScreenWidth => 1600;
        public int ScreenHeight => 900;

        public const double ScaleHack = 1.5;
        public static GraphicsDevice GlobalGraphicsDevice;
        public static GraphicsDeviceManager GlobalGraphicsDeviceManager;
        public static RenderTarget2D WorldRenderTarget;


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

            GlobalGraphicsDevice = GraphicsDevice;
            WorldRenderTarget = new RenderTarget2D(GraphicsDevice, (int)(ScreenWidth * ScaleHack), (int)(ScreenHeight * ScaleHack));
            GlobalGraphicsDeviceManager = _graphics;

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
                Content.Load<Texture2D>("Planet1"),
                Content.Load<Texture2D>("Planet2"),
                Content.Load<Texture2D>("Planet3"),
                Content.Load<Texture2D>("Planet4"),
                Content.Load<Texture2D>("Planet5"),
                Content.Load<Texture2D>("Planet6"),
                Content.Load<Texture2D>("Planet7"),
            };
            Resources.BlueEngine = new List<Texture2D>(){
                Content.Load<Texture2D>("Engines/blueng-512"),
                Content.Load<Texture2D>("Engines/blueng1-512"),
                Content.Load<Texture2D>("Engines/blueng2-512"),
                Content.Load<Texture2D>("Engines/blueng3-512"),
                Content.Load<Texture2D>("Engines/blueng4-512")
            };
            Resources.RedEngine = new List<Texture2D>(){
                Content.Load<Texture2D>("Engines/redeng-512"),
                Content.Load<Texture2D>("Engines/redeng1-512"),
                Content.Load<Texture2D>("Engines/redeng2-512"),
                Content.Load<Texture2D>("Engines/redeng3-512"),
                Content.Load<Texture2D>("Engines/redeng4-512")
            };
            Resources.PulsingPlanet = new List<Texture2D>(){
                Content.Load<Texture2D>("planet2-l1"),
                Content.Load<Texture2D>("planet2-l2"),
                Content.Load<Texture2D>("planet2-l3"),
            };
            Resources.PlayButton = Content.Load<Texture2D>("Bottoms/play1");
            Resources.PlayButtonHover = Content.Load<Texture2D>("Bottoms/play2");
            Resources.ReplayButton = Content.Load<Texture2D>("Bottoms/replay1");
            Resources.ReplayButtonHover = Content.Load<Texture2D>("Bottoms/replay2");
            Resources.SubmitButton = Content.Load<Texture2D>("Bottoms/sumbit1");
            Resources.SubmitButtonHover = Content.Load<Texture2D>("Bottoms/sumbit2");
            Resources.Slider = Content.Load<Texture2D>("Bottoms/slider");
            Resources.SliderHover = Content.Load<Texture2D>("Bottoms/slider_highlight");
            Resources.UIBackground = Content.Load<Texture2D>("Bottoms/ui_base");
            Resources.LeftLabel = Content.Load<Texture2D>("Bottoms/left");
            Resources.RightLabel = Content.Load<Texture2D>("Bottoms/right");
            Resources.WeaponsLabel = Content.Load<Texture2D>("Bottoms/weapons");
            Resources.BarEmpty = Content.Load<Texture2D>("Bottoms/bar_empty");
            Resources.BarFull = Content.Load<Texture2D>("Bottoms/bar_full");
            Resources.BarRed = Content.Load<Texture2D>("Bottoms/bar_red");
            Resources.BlueExplosion = Enumerable.Range(1, 10)
                                                .Select(i => "Explosions/blue/" + i)
                                                .Select(s => Content.Load<Texture2D>(s))
                                                .ToList();
            Resources.RedExplosion = Enumerable.Range(1, 10)
                                                .Select(i => "Explosions/red/" + i)
                                                .Select(s => Content.Load<Texture2D>(s))
                                                .ToList();
            var backgroundOptions = new List<string>()
            {
                "Background/something_right1",
                "Background/something_left2",
                "Background/something_top3",
                "Background/something_bottom4",
                "Background/something_front5",
                "Background/something_back6",
            };
            Resources.Background = Content.Load<Texture2D>(backgroundOptions[new Random().Next(backgroundOptions.Count)]);
            Resources.RedTurnIndicator = Content.Load<Texture2D>("Bottoms/redturn");
            Resources.BlueTurnIndicator = Content.Load<Texture2D>("Bottoms/blueturn");

            var world = new World(ScreenWidth, ScreenHeight);
            world.Ships.Add(new Ship(new Vector(100, 100), Models.BlueModel));
            world.Ships.Add(new Ship(new Vector(130, 400), Models.BlueModel));
            world.Ships.Add(new Ship(new Vector(115, 560), Models.BlueModel));

            world.Ships.Add(new Ship(new Vector(1600 * 1.5 - 100, 1000 - 100), Models.RedModel));
            world.Ships.Add(new Ship(new Vector(1600 * 1.5 - 130, 1000 - 400), Models.RedModel));
            world.Ships.Add(new Ship(new Vector(1600 * 1.5 - 115, 1000 - 560), Models.RedModel));
            //world.Ships.Add(new Ship(new Vector(150, 400), Models.BlueModel));

            world.Planets.Add(new Planet(new Vector(600, 500), 100, 2));
            //world.Planets.Add(new Planet(new Vector(900, 800), 50, 1));
            //world.Planets.Add(new Planet(new Vector(950, 100), 50, 4));
            //world.Planets.Add(new Planet(new Vector(1100, 680), 100, 5));
            world.Planets.Add(new BlendedPlanet(new Vector(1100, 680), 100));
            //world.Planets.Add(new Planet(new Vector(300, 800), 90, 1));
            world.Planets.Add(new Planet(new Vector(1700, 300), 50, 4));
            //world.Planets.Add(new Planet(new Vector(850, 600), 90, 2));
            world.Planets.Add(new PulsingPlanet(new Vector(1650, 1000), 90));

            var playbackManager = new PlaybackManager();

            //_currentScene = new GameScene(this, world, playbackManager);
            _currentScene = new HotseatScene(this, world, playbackManager);
        }
        
        protected override void Update(GameTime gameTime)
        {
            GlobalTimer += 1 / 60.0;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _currentScene.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _currentScene.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        public void SetScene(IScene scene)
        {
            _currentScene = scene;
        }
    }
}
