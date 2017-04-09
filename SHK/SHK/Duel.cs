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
        static Vector2 charP = new Vector2(200, 100);
        static Vector2 charS = new Vector2(128f, 128f);

        public Character player2;
        static Vector2 char2P = new Vector2(1208, 100);
        static Vector2 char2S = new Vector2(128f, 128f);



        public Duel()
        {
            player1 = new Character("ryu", charP, charS, 1, 1, 0, SpriteEffects.None, false);

            player2 = new Character("ryu", char2P, char2S, 1, 1, 0, SpriteEffects.FlipHorizontally, true);
        }   

        public void Draw()
        {
            player1.Draw();

            player2.Draw();
        }

        public void Update()
        {
            player1.Update();
            player2.Update();
        }

    }
}
