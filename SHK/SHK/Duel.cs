using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SHK
{
    class Duel
    {
        public Character player1;
        static Vector2 charP = new Vector2(100, 100);
        static Vector2 charS = new Vector2(128f, 128f);
        
        public Duel()
        {
            player1 = new Character("ryu", charP, charS, 1, 1, 0);
        }   

        public void Draw()
        {
            player1.Draw();
        }

    }
}
