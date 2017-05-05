using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    public class AttackList
    {
        public int damage;
        //private Character player;
        public Rectangle hitbox;
        private Texture2D hitbox_text;
        private bool endAttack = true;
        private int attackDuration;
        private TimeSpan delay;
        private Vector2 positionHitbox;

        public AttackList()
        {

        }


        public void LightPunch(Vector2 position)
        {
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            positionHitbox = new Vector2(position.X + 10, (position.Y + 170f));
            SetDrawHitbox(positionHitbox);
        }

        public void MediumPunch(Vector2 position)
        {
            damage = 7;
            endAttack = false;
            attackDuration = 1000;

            positionHitbox = new Vector2(position.X + 10, (position.Y + 170f));
            SetDrawHitbox(positionHitbox);
        }

        public void HeavyPunch(Vector2 position)
        {
            damage = 10;
            endAttack = false;
            attackDuration = 1000;

            positionHitbox = new Vector2(position.X + 10, (position.Y + 170f));
            SetDrawHitbox(positionHitbox);
        }

        public void LightKick(Vector2 position)
        {
            damage = 7;
            endAttack = false;
            attackDuration = 1000;

            positionHitbox = new Vector2(position.X + 10, (position.Y + 170f));
            SetDrawHitbox(positionHitbox);
        }

        public void MediumKick(Vector2 position)
        {
            damage = 10;
            endAttack = false;
            attackDuration = 1000;

            positionHitbox = new Vector2(position.X + 10, (position.Y + 170f));
            SetDrawHitbox(positionHitbox);
        }

        public void HeavyKick(Vector2 position)
        {
            damage = 13;
            endAttack = false;
            attackDuration = 1000;

            positionHitbox = new Vector2(position.X + 10, (position.Y + 170f));
            SetDrawHitbox(positionHitbox);
        }

        
        public void SetDrawHitbox(Vector2 positionHitbox)
        {
            hitbox = Camera.ComputePixelRectangle(positionHitbox, (Vector2.UnitX * 100) + (Vector2.UnitY * 30));
            hitbox_text = new Texture2D(Game1.mGraphics.GraphicsDevice, hitbox.Width, hitbox.Height);
            Color[] data = new Color[hitbox.Width * hitbox.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            hitbox_text.SetData(data);
        }

        public void Draw(GameTime gameTime)
        {
            delay = gameTime.TotalGameTime;
            if(hitbox_text != null && !endAttack && !((gameTime.TotalGameTime.TotalMilliseconds - delay.TotalMilliseconds) > attackDuration))
            {
                Game1.sSpriteBatch.Draw(hitbox_text, hitbox, Color.Black);
                endAttack = true;
            }
        }
    }
}
