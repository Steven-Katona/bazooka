﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FakeArcade2.GameStuff;

namespace FakeArcade2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        RenderTarget2D _nativeTarget;
        Rectangle boxingRect;
        Rectangle _nativeRectangle;
        Level currentLevel;
        int maxWidth;
        int maxHeight;
        int Level = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            AnimationConstruction.Initilize(); //Required
            maxHeight = _graphics.PreferredBackBufferHeight;
            maxWidth = _graphics.PreferredBackBufferWidth;
            _nativeRectangle = new(0, 0, maxWidth, maxHeight);
            _nativeTarget = new RenderTarget2D(GraphicsDevice, _nativeRectangle.Width, _nativeRectangle.Height);
            boxingRect = _nativeRectangle;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            currentLevel = new Level(Services, Content, GraphicsDevice, Content.Load<Texture2D>("level2d/"+ Level.ToString()), maxWidth, maxHeight);
            (int, int) dem = currentLevel.levelDimensions;
            _nativeRectangle = new(0, 0, dem.Item1, dem.Item2);
            _nativeTarget = new RenderTarget2D(GraphicsDevice, _nativeRectangle.Width, _nativeRectangle.Height);
            boxingRect = _nativeRectangle;



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                currentLevel.Dispose();
                Exit();
            }

            var state = Keyboard.GetState();

            if(currentLevel.the_Player.at_Exit)
            {
                Level += currentLevel.the_Player.exit_found;
                currentLevel.Dispose();
                
                try
                {
                    currentLevel = new Level(Services, Content, GraphicsDevice, Content.Load<Texture2D>("level2d/" + Level.ToString()), maxWidth, maxHeight);
                }
                catch
                {
                    Exit();
                }
            }
            currentLevel.Update(gameTime, state);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.SetRenderTarget(_nativeTarget);
            GraphicsDevice.Clear(Color.Blue);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: currentLevel.getViewOffset());

            currentLevel.Draw(gameTime, _spriteBatch, GraphicsDevice);

            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Blue);


            _spriteBatch.Begin();
            _spriteBatch.Draw(_nativeTarget, boxingRect, Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}