using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoWeDig.Data;
using SoWeDig.Utilities;

namespace SoWeDig
{
    public class SoWeDig : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteEngine spriteEngine;
        private World world;
        private Camera camera;
        private int previousScrollValue;

        public SoWeDig()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferMultiSampling = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.PreferredBackBufferWidth = Settings.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Settings.SCREEN_HEIGHT;
            graphics.IsFullScreen = Settings.FULLSCREEN;
            graphics.ApplyChanges();

            camera = new Camera(graphics.GraphicsDevice);
            IsMouseVisible = false;
            previousScrollValue = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteEngine = new SpriteEngine(Content);

            world = new World(50, 100);

            camera.LoadContent();
            camera.Zoom = Settings.CAMERA_ZOOM;
            //camera.Zoom = 0.015f;
            camera.Position = world.Player.Position + new Vector2(Settings.TILE_SIZE / 2, Settings.TILE_SIZE / 2);
            camera.Debug.Grid.AddLines(Settings.TILE_SIZE, Color.White, 4);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Utilities.Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape)) Exit();

            world.Update(gameTime, keyboard);
            camera.Position = world.Player.Position + new Vector2(Settings.TILE_SIZE / 2, Settings.TILE_SIZE / 2);

            if (mouse.ScrollWheelValue > previousScrollValue) camera.Zoom += 0.1f;
            else if (mouse.ScrollWheelValue < previousScrollValue) camera.Zoom -= 0.1f;

            if (camera.Zoom < 0.5) camera.Zoom = 0.5f;
            else if (camera.Zoom > 1.5) camera.Zoom = 1.5f;

            previousScrollValue = mouse.ScrollWheelValue;

            camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(camera, samplerState: SamplerState.PointClamp);
            world.Draw(spriteBatch, spriteEngine);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
