using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    class Projectil : SpritePrimitive
    {
        public Projectil(Vector2 position, Vector2 size) : base("a", position, size, 0, 0, 0, SpriteEffects.None)
        {
            
        }
    }
}
