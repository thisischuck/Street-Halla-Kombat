using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private Vector2 sizeHitbox;

        public AttackList()
        {

        }


        public void LightPunch(Vector2 position, SpriteEffects spriteffects)
        {
            delay = Game1.gameTime.TotalGameTime;
            int pos = -95;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -205;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 170f));
            SetDrawHitbox(positionHitbox, sizeHitbox, false);
        }

        public void MediumPunch(Vector2 position, SpriteEffects spriteffects)
        {
            delay = Game1.gameTime.TotalGameTime;
            int pos = -95;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -205;
            damage = 7;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 170f));
            SetDrawHitbox(positionHitbox, sizeHitbox, false);
        }

        public void HeavyPunch(Vector2 position, SpriteEffects spriteffects)
        {
            delay = Game1.gameTime.TotalGameTime;
            int pos = -95;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -205;
            damage = 10;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 170f));
            SetDrawHitbox(positionHitbox, sizeHitbox, false);
        }

        public void LightKick(Vector2 position, SpriteEffects spriteffects)
        {
            delay = Game1.gameTime.TotalGameTime;
            int pos = -95;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -205;
            damage = 7;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 170f));
            SetDrawHitbox(positionHitbox, sizeHitbox, false);
        }

        public void MediumKick(Vector2 position, SpriteEffects spriteffects)
        {
            delay = Game1.gameTime.TotalGameTime;
            int pos = -95;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -205;
            damage = 10;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 170f));
            SetDrawHitbox(positionHitbox, sizeHitbox, false);
        }

        public void HeavyKick(Vector2 position, SpriteEffects spriteffects)
        {
            delay = Game1.gameTime.TotalGameTime;
            int pos = -95;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -205;
            damage = 13;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 170f));
            SetDrawHitbox(positionHitbox, sizeHitbox, false);
        }

        public void LightPunchAir(Vector2 position, SpriteEffects spriteffects)
        {
            delay = Game1.gameTime.TotalGameTime;
            int pos = -95;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -205;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 170f));
            SetDrawHitbox(positionHitbox, sizeHitbox, false);
        }

        public void Hadouken(Vector2 position, SpriteEffects spriteffects)
        {
            delay = Game1.gameTime.TotalGameTime;
            int pos = -95;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -205;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(500, 500);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 100f));
            SetDrawHitbox(positionHitbox, sizeHitbox, true);
        }


        //------------------------------------------------------------------------------------------------------


        public void SetDrawHitbox(Vector2 positionHitbox, Vector2 sizeHitbox, bool moving)
        {
            hitbox = Camera.ComputePixelRectangle(positionHitbox, sizeHitbox);
            hitbox_text = new Texture2D(Game1.mGraphics.GraphicsDevice, hitbox.Width, hitbox.Height);
            Color[] data = new Color[hitbox.Width * hitbox.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            hitbox_text.SetData(data);
            if (moving == true)
            {
                while (hitbox.X > 0 && hitbox.X < 1280)
                {
                    hitbox.X += 100;
                }
            }
        }

        public void Draw()
        {
            if(hitbox_text != null && !endAttack)
            {
                Game1.sSpriteBatch.Draw(hitbox_text, hitbox, Color.Black);
                if ((Game1.gameTime.TotalGameTime.TotalMilliseconds - delay.TotalMilliseconds) > attackDuration)
                    endAttack = true;                
            }
            else
            {
                hitbox.Location = new Point(-100,-100);
            }
        }
    }
}
