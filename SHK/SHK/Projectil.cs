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
        private Vector2 position, size;
        private SpriteEffects effects;

        public Projectil(Vector2 position, Vector2 size, int row, int colum, SpriteEffects effect) : base("hadouken", position, size,row, colum, 0, effect)
        {
            this.position = position;
            this.size = size;
            this.effects = effect;
        }


    }
}
