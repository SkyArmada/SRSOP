using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tiled;
using System.Collections.Generic;

namespace ExtendedTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        private Camera2D _camera;
        public CircleF test;
        private TiledMap _map;
        private TiledMapRenderer _mapRenderer;
        List<Sprite> gameObjectList;
        MouseState previousMouseState;
        Sprite mouseCursor;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            gameObjectList = new List<Sprite>();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent("Art/Player", this);
            player._Position = new Vector2(150, 150);

            Tree testTree = new Tree(Tree.TreeType.kNormalTree);
            testTree.LoadContent("Art/tree", this);
            testTree._Position = new Vector2(300, 300);
            testTree._Tag = Sprite.SpriteType.kTreeType;
            testTree._CurrentState = Sprite.SpriteState.kStateActive;
            gameObjectList.Add(testTree);

            mouseCursor = new Sprite();
            mouseCursor.LoadContent("Art/log", this);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(this.IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                MouseState mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed)
                {
                    mouseCursor._Position = new Vector2(mouseState.Position.X, mouseState.Position.Y);
                }
                player.Update(gameTime, gameObjectList);
                foreach (Sprite sprite in gameObjectList)
                {
                    sprite.Update(gameTime, gameObjectList);
                }
                // TODO: Add your update logic here

                base.Update(gameTime);

            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            foreach (Sprite sprite in gameObjectList)
            {
                sprite.Draw(spriteBatch);
            }
            // TODO: Add your drawing code here
            mouseCursor.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
