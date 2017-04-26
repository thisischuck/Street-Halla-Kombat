using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    class Duel
    {

        string player1Choice;
        string player2Choice;

        public Character player1;
        public HealthBar hpPlayer1;
        static Vector2 hpPosition1 = new Vector2(10, 100);
        static Vector2 charP = new Vector2(200, 100);
        static Vector2 charS = new Vector2(128f, 128f);

        public Character player2;
        public HealthBar hpPlayer2;
        static Vector2 hpPosition2 = new Vector2(200, 100);
        static Vector2 char2P = new Vector2(1208, 100);
        static Vector2 char2S = new Vector2(128f, 128f);

        public Plataforma ChaoPlataforma;
        static Vector2 platPosition = new Vector2(640,50);
        static Vector2 platSize = new Vector2(1280,100);


        public Duel()
        {
            player1 = new Character("test1", charP, charS, 2, 4, 0, 1, SpriteEffects.None, false);
            hpPlayer1 = new HealthBar(Game1.sContent, player1, hpPosition1, SpriteEffects.None);

            player2 = new Character("test1", char2P, charS, 2, 4, 0, 2, SpriteEffects.None, false);
            ChaoPlataforma = new Plataforma(false, platSize, platPosition, player1,player2);
            hpPlayer2 = new HealthBar(Game1.sContent, player2, hpPosition2, SpriteEffects.FlipHorizontally);
        }   

        public void Draw()
        {
            player1.Draw();
            hpPlayer1.Draw(Game1.sSpriteBatch);

            player2.Draw();
            ChaoPlataforma.Draw();
            hpPlayer2.Draw(Game1.sSpriteBatch);
        }

        public void Update()
        {
            player1.Update();
            hpPlayer1.Update();

            player2.Update();
            hpPlayer2.Update();

            ChaoPlataforma.Update();
        }

    }
}
