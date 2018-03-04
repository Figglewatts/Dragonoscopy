using System;
using Dragonoscopy.Game.Scenes;
using Dragonoscopy.Global;
using Dragonoscopy.Graphics;
using Dragonoscopy.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dragonoscopy
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Dragonoscopy : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private TestScene scene;
        private SceneManager sceneManager;

        private PixelatedViewport Viewport;

        private const double dt = 1.0f / 60.0f; // 60fps
        
        public Dragonoscopy()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Settings.VIRTUAL_VIEWPORT_WIDTH * 2;
            graphics.PreferredBackBufferHeight = Settings.VIRTUAL_VIEWPORT_HEIGHT * 2;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Resize;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            TargetElapsedTime = TimeSpan.FromSeconds(dt);
        }

        public void Resize(object sender, EventArgs e)
        {
            //graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            //graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;

            Viewport.Resize(new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));
        }

        protected override void Initialize()
        {
            sceneManager = new SceneManager();
            scene = new TestScene(Content, graphics.GraphicsDevice);
            sceneManager.AddScene(scene);

            Viewport = new PixelatedViewport(graphics, new Vector2(Settings.VIRTUAL_VIEWPORT_WIDTH, Settings.VIRTUAL_VIEWPORT_HEIGHT));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Viewport.PixelRenderTarget);
            GraphicsDevice.Clear(new Color(6, 6, 8, 255));

            sceneManager.Draw();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

            spriteBatch.Draw(Viewport.PixelRenderTarget, new Rectangle(Viewport.Viewport.X, Viewport.Viewport.Y, Viewport.Viewport.Width, Viewport.Viewport.Height), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
