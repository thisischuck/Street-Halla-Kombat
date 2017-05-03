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
        private int damage;
        private Character player;
        private Rectangle a;

        public AttackList()
        {
            //player = ;
        }


        public void LightPunch()
        {
            damage = 5;
            a = Camera.ComputePixelRectangle(player.position, Vector2.One * 100);
            
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
    }
}
