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
    class Char : SpritePrimitive
    {
        Vector2 position;
        Vector2 size;

        public Char(string imageName,Vector2 cposition, Vector2 csize) : base(imageName,cposition,csize,0,0,0)
        {  
            this.position = cposition;
            this.size = csize;
        }

        private enum CharState
        {
            idle,
            dead,
            air,
            stunned,    
            mKick,
            mPunch,
            lPunch,
            lKick,
            hPunch,
            hKick
        }
        private CharState mCurrentCharState;


        public override void Draw()
        {
            base.Draw();
            switch (mCurrentCharState)
            {
                case CharState.idle:

                    break;

                case CharState.dead:

                    break;

                case CharState.air:

                    break;

                case CharState.stunned:

                    break;

                case CharState.mKick:

                    break;

                case CharState.lKick:

                    break;

                case CharState.hKick:

                    break;

                case CharState.mPunch:

                    break;

                case CharState.lPunch:

                    break;

                case CharState.hPunch:

                    break;
            }

        }//falta por as animaçoes

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
    }
}
