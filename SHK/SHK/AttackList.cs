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
        public Rectangle a;
        private Texture2D a_text;
        private bool endAttack = true;
        private int attackDuration;
        private TimeSpan delay;

        public AttackList()
        {

        }


        public void LightPunch(Vector2 position)
        {
            endAttack = false;
            attackDuration = 1000;
            damage = 5;

            Vector2 b = new Vector2(position.X + 10, (position.Y + 190f));
            a = Camera.ComputePixelRectangle(b, (Vector2.UnitX * 100) + (Vector2.UnitY * 30));
            a_text = new Texture2D(Game1.mGraphics.GraphicsDevice, a.Width, a.Height);
            Color[] data = new Color[a.Width * a.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            a_text.SetData(data);

        }

        public void MediumPunch()
        {
            damage = 7;
        }

        public void HeavyPunch()
        {
            damage = 10;
        }

        public void LightKick()
        {
            damage = 7;
        }

        public void MediumKick()
        {
            damage = 10;
        }

        public void HeavyKick()
        {
            damage = 13;
        }

        
        public void SetDrawHitbox()
        {

        }

        public void Draw(GameTime gameTime)
        {
            delay = gameTime.TotalGameTime;
            if(a_text != null && !endAttack && !((gameTime.TotalGameTime.TotalMilliseconds - delay.TotalMilliseconds) > attackDuration))
            {
                Game1.sSpriteBatch.Draw(a_text, a, Color.Black);
                endAttack = true;
            }
        }
    }
}
