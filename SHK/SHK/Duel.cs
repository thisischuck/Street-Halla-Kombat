using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SHK
{
    class Duel 
    {

        string player1Choice;
        string player2Choice;

        public Character player1;
        public HealthBar hpPlayer1;

        static Vector2 hpSize = new Vector2(700, 50);
        static Vector2 hpPosition1 = new Vector2(500, 500);
    
        static Vector2 charP = new Vector2(200, 100);
        static Vector2 charS = new Vector2(128f, 128f);

        public Character player2;
        public HealthBar hpPlayer2;

        static Vector2 char2P = new Vector2(1208, 100);
        static Vector2 char2S = new Vector2(60, 106);

        static Vector2 hpPosition2 = new Vector2(0, 0);

        public Plataforma ChaoPlataforma;
        public Plataforma ChaoPlataforma2;
        static Vector2 platPosition = new Vector2(1000,100);
        static Vector2 platPosition2 = new Vector2(200, 200);
        static Vector2 platSize = new Vector2(510,65);

        GameTimer timer;

        public Duel()
        {
             player1 = new Character("Untitled", charP, charS, 18, 1, 0, 1, SpriteEffects.None, false);
         //   hpPlayer1 = new HealthBar(player1, hpPosition1,hpSize, SpriteEffects.None);

            player2 = new Character("Untitled", char2P, char2S, 18, 1, 0, 2, SpriteEffects.None, false);
            hpPlayer2 = new HealthBar(player2, hpPosition2,hpSize, SpriteEffects.FlipHorizontally);


            ChaoPlataforma = new Plataforma(false, platSize, platPosition, player1, player2);
            //ChaoPlataforma2 = new Plataforma(false,platSize, platPosition2, player1, player2);
        }   

        public void LoadContent()
        {
            timer = new GameTimer(180.0f);
            timer.Font = Game1.sContent.Load<SpriteFont>("Arial");
            timer.Position = new Vector2((Game1.mGraphics.PreferredBackBufferWidth / 2) - timer.Font.MeasureString(timer.Text).X / 2, 0);
        }

        public void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
          //  player1.Update();
          //  hpPlayer1.Update();

            player2.Update();
            hpPlayer2.Update();
            ChaoPlataforma.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            timer.Draw(spriteBatch);
            //hpPlayer1.Draw();
            hpPlayer2.Draw();
            //player1.Draw();


            player2.Draw();
            ChaoPlataforma.Draw();

        }

    }
}
