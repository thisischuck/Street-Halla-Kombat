using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    public class Projectil : SpritePrimitive
    {
        private Vector2 position, size;
        private SpriteEffects effects;
        public float speed;
        public Rectangle hitbox;
        private Texture2D a_text;

        public Projectil(Vector2 position, Vector2 size, int row, int colum, SpriteEffects effect) : base("ryu", position, size,row, colum, 0, effect)
        {
            this.position = position;
            this.size = size;
            this.effects = effect;
            this.speed = 5;

            hitbox = Camera.ComputePixelRectangle(position, size);
            a_text = new Texture2D(Game1.mGraphics.GraphicsDevice, hitbox.Width, hitbox.Height);

            Color[] data = new Color[hitbox.Width * hitbox.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            a_text.SetData(data);

            Velocity = (Vector2.UnitX * speed) + (Vector2.UnitY);
        }

        public override void Draw()
        {
            Game1.sSpriteBatch.Draw(a_text, hitbox, Color.White);
            base.Draw();
        }
    }
}
