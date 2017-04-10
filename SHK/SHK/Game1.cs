﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Threading;

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
        static public AudioManager sAudio;
        public Song song;
        public float songVolume = 0.2f;
        protected Random rnd = new Random();
        public List<Song> listaMusicas = new List<Song>();



        // Prefer window size
        // Convention: "k" to begin constant variable names
        const int kWindowWidth = 1280;
        const int kWindowHeight = 720;
        #endregion


        SoundEffect PunchMiss;
        SoundEffect PunchHit;

        MainMenu main = new MainMenu();

        public Game1()
        {

            sAudio = new AudioManager();
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

            #region Carregar menu
            main.LoadContent(Content);

            /*menu = new GUIElement("menu");
             menu.CenterElement(kWindowHeight, kWindowWidth);*/
            #endregion
            
            #region Carregar sons e efeitos

            /*
            Carrega as músicas para uma lista
            */
            listaMusicas.Add(sContent.Load<Song>("Metallica - Master Of Puppets"));
            listaMusicas.Add(sContent.Load<Song>("Motörhead - King of Kings (Triple H)"));



            /*
            Carrega os efeitos sonoros  
            */
            PunchHit = sContent.Load<SoundEffect>("PunchHit");
            PunchMiss = sContent.Load<SoundEffect>("PunchMiss");
            #endregion


            /*sAudio.PlaySoundEffectRandomPitch(PunchHit, 1f);  //teste
            sAudio.PlayRandomSong(listaMusicas);  //teste
            */
            // Define camera window bounds
            Camera.SetCameraWindow(new Vector2(0, 0), kWindowWidth);


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
            main.Update();
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

            main.Draw(sSpriteBatch);

            Game1.sSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
