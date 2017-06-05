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
        public int damage;
        public float speed;
        public Rectangle hitbox;
        private Texture2D a_text;

        public Projectil(Vector2 position, Vector2 size, int row, int colum, SpriteEffects effect) : base("BrazilianHadouken", position, size,row, colum, 0, effect)
        {
            this.position = position;
            this.size = size;
            this.effects = effect;
            this.speed = 20;
            this.damage = 20;

            Vector2 a = new Vector2(mPosition.X - mSize.X / 2, mPosition.Y + mSize.Y / 2 - 40);
            Vector2 b = new Vector2(mSize.X / 2 - 65, mSize.Y - mSize.Y / 3);

            hitbox = Camera.ComputePixelRectangle(a, b);
            a_text = new Texture2D(Game1.mGraphics.GraphicsDevice, hitbox.Width, hitbox.Height);

            Color[] data = new Color[hitbox.Width * hitbox.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            a_text.SetData(data);

            if(effect != SpriteEffects.FlipHorizontally)
                Velocity = (Vector2.UnitX * speed) + (Vector2.Zero);
            else Velocity = (Vector2.UnitX * -speed) + (Vector2.Zero);
        }

        public override void Update()
        {
            Vector2 a = new Vector2(mPosition.X - mSize.X / 2, mPosition.Y + mSize.Y / 2 - 40);
            //Vector2 a = new Vector2(mPosition.X, mPosition.Y);
            Vector2 b = new Vector2(mSize.X / 2 - 65, mSize.Y - mSize.Y / 3);
            hitbox = Camera.ComputePixelRectangle(a, b);
            base.Update();
        }


        public override void Draw()
        {
            base.Draw();
            Game1.sSpriteBatch.Draw(a_text, hitbox, Color.White);
        }
    }
}
