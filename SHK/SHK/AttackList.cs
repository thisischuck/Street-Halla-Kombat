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
        public bool endAttack = true;
        private int attackDuration;
        private Vector2 positionHitbox;
        private Vector2 sizeHitbox;

        public AttackList()
        {

        }


        //==========================================================================
        //GROUNDED
        //==========================================================================   
        
             
        public void LightPunch(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -73;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -220;
            damage = 2;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 170f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void MediumPunch(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -90;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -210;
            damage = 4;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 160f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void HeavyPunch(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -90;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -210;
            damage = 7;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 150f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void LightKick(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -75;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -225;
            damage = 3;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(65, 65);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 50f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void MediumKick(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -75;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -225;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(65, 45);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 200f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void HeavyKick(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -90;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -220;
            damage = 8;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2(position.X + pos, (position.Y + 200f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }


        //==========================================================================
        //AIR
        //==========================================================================


        public void LightPunchAir(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -90;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -210;
            damage = 2;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(65, 65);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 70f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void MediumPunchAir(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -110;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -180;
            damage = 4;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(45, 65);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 200f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void HeavyPunchAir(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -75;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -225;
            damage = 7;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(65, 65);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 45f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void LightKickAir(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -80;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -220;
            damage = 3;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(65, 65);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 35f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void MediumKickAir(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -90;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -210;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(40, 70);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 210f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void HeavyKickAir(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -65;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -235;
            damage = 8;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(65, 65);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 25f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        //==========================================================================
        //CROUCHED
        //==========================================================================

        public void LightPunchCrouched(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -75;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -225;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 110f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void MediumPunchCrouched(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -90;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -210;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(100, 30);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 100f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void HeavyPunchCrouched(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -110;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -180;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(45, 65);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 230f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void LightKickCrouched(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -70;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -230;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(120, 40);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 30f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void MediumKickCrouched(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -70;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -230;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(120, 40);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 30f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        public void HeavyKickCrouched(Vector2 position, SpriteEffects spriteffects)
        {
            int pos = -60;
            if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                pos = -240;
            damage = 5;
            endAttack = false;
            attackDuration = 1000;

            sizeHitbox = new Vector2(120, 40);
            positionHitbox = new Vector2((position.X + pos), (position.Y + 30f));
            SetDrawHitbox(positionHitbox, sizeHitbox);
        }

        //------------------------------------------------------------------------------------------------------


        public void SetDrawHitbox(Vector2 positionHitbox, Vector2 sizeHitbox)
        {
            hitbox = Camera.ComputePixelRectangle(positionHitbox, sizeHitbox);
            hitbox_text = new Texture2D(Game1.mGraphics.GraphicsDevice, hitbox.Width, hitbox.Height);
            Color[] data = new Color[hitbox.Width * hitbox.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            hitbox_text.SetData(data);
        }

        public void Draw()
        {
            if(hitbox_text != null && !endAttack)
            {
                Game1.sSpriteBatch.Draw(hitbox_text, hitbox, Color.Black);
                attackDuration = 0;
                while (attackDuration < 1000)
                {
                    attackDuration++;
                    endAttack = true;
                }     
            }
            else
            {
                hitbox.Location = new Point(-100,-100);
            }
        }
    }
}
