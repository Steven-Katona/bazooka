using Microsoft.Xna.Framework;
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
        Level test;
        KeyboardState key_state;
        int maxWidth;
        int maxHeight;
    


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
            test = new Level(Services, Content, GraphicsDevice, Content.Load<Texture2D>("level2d/test2_level"), maxWidth, maxHeight);
            (int, int) dem = test.levelDimensions;
            _nativeRectangle = new(0, 0, dem.Item1, dem.Item2);
            _nativeTarget = new RenderTarget2D(GraphicsDevice, _nativeRectangle.Width, _nativeRectangle.Height);
            boxingRect = _nativeRectangle;



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            test.Update(gameTime, key_state);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_nativeTarget);
            GraphicsDevice.Clear(Color.Blue);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            

            test.Draw(gameTime, _spriteBatch);

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