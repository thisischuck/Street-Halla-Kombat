using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace SHK
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        #region Variaveis Globais
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static public GraphicsDeviceManager mGraphics;
        public Song song;
        protected Random rnd = new Random();
        static public SpriteBatch sSpriteBatch;  // Drawing support
        static public ContentManager sContent;   // Loading textures
        static public GraphicsDeviceManager sGraphics; // Current display size
        #endregion

        #region Tamanho da janela
        // Prefer window size
        // Convention: "k" to begin constant variable names
        const int kWindowWidth = 1280;
        const int kWindowHeight = 720;
        #endregion 

        public Game1()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Create graphics device to access window size
            Game1.sGraphics = new GraphicsDeviceManager(this);
            // set prefer window size
            Game1.sGraphics.PreferredBackBufferWidth = kWindowWidth;
            Game1.sGraphics.PreferredBackBufferHeight = kWindowHeight;
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sSpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            int randomSong = rnd.Next(1, 2); //min <= rnd < max

            switch (randomSong)
            {
                case 1:
                    this.song = Content.Load<Song>("Songs//Metallica - Master Of Puppets");
                    MediaPlayer.Volume = 0.1f;
                    MediaPlayer.Play(song);
                    break;

                case 2:
                    break;
            }
            // Define camera window bounds
            Camera.SetCameraWindow(new Vector2(10f, 20f), 100f);

            // Create the primitives
            /*mGraphicsObjects = new TexturedPrimitive[kNumObjects];
            mGraphicsObjects[0] = new TexturedPrimitive(
                     "UWB-JPG", // Image file name
                     new Vector2(15f, 25f), // Position to draw
                     new Vector2(10f, 10f));*/

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
