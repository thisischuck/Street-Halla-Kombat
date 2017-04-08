using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SHK
{
    class Char : TexturedPrimitive
    {
        //animation bools Not sure yet
        bool attack = false;
        bool jumping = false;
        bool idle = false;

        public Char() : base("ryu")
        {
            //ouch
        }

        /*public enum CharacterStance { Normal_Stance }

        public struct Textures
        {
            public struct Nomal_Stance
            {
                public static List<Texture2D> Textures_array = new List<Texture2D>();
            }
        }

        public static void LoadTextureFrame(ref List<Texture2D> list, Texture2D frame)
        {

        }
        */
    }
}
