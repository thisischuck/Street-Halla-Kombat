using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace SHK
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region cenas que e preciso
        static public GraphicsDeviceManager mGraphics;
        static public SpriteBatch sSpriteBatch;
        static public ContentManager sContent;
        public Song song;
        public float songVolume = 0.2f;
        protected Random rnd = new Random();
        public List<Song> listaMusicas = new List<Song>();
        public List<SoundEffect> soundEffects = new List<SoundEffect>();



        // Prefer window size
        // Convention: "k" to begin constant variable names
        const int kWindowWidth = 1280;
        const int kWindowHeight = 720;
        #endregion

        public Song song;
        protected Random rnd = new Random();
        Char player1;
        Vector2 charP = new Vector2(720, 360);
        Vector2 charS = new Vector2(100, 100);

        public Game1()
        {
            

            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Game1.sContent = Content;

            // set prefer window size
            Game1.mGraphics.PreferredBackBufferWidth = kWindowWidth;
            Game1.mGraphics.PreferredBackBufferHeight = kWindowHeight;

            
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
            MediaPlayer.Volume = songVolume;

            #region Carregar sons e efeitos

            /*
            Carrega as músicas para uma lista
            */
            listaMusicas.Add(Content.Load<Song>("Metallica - Master Of Puppets"));
            listaMusicas.Add(Content.Load<Song>("Motörhead - King of Kings (Triple H)"));



            /*
            Carrega os efeitos sonoros  
            */

            #endregion
            player1 = new Char("ryu", charP, charS,1,1,0);
            // Define camera window bounds
            Camera.SetCameraWindow(new Vector2(10f, 20f), 100f);

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

            player1.Update();

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
            Game1.sSpriteBatch.Begin();

            // TODO: Add your drawing code here
            player1.Draw();

            Game1.sSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
