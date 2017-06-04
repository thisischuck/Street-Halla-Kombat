using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHK
{
    class MapSelection
    {
        Duel a;

        Texture2D Museu;
        Texture2D Desert;
        Texture2D Mountains;
        Rectangle desertRec;
        Rectangle mountainRec;
        Rectangle museuRec;
        string selected;

        Character c;
        static Vector2 cPosition = new Vector2(1200, 100);
        static Vector2 cSize = new Vector2(400, 400);
        private List<Plataforma> plataforma = new List<Plataforma>();
        public AttackList attacks = new AttackList();

        // plataforma
        public Plataforma ChaoPlataforma;
        static Vector2 platSize = new Vector2(3500, 65);
        static Vector2 platPosition = new Vector2(1000, 100);

        public MapSelection()
        {
            Mountains = Game1.sContent.Load<Texture2D>("Museu");// AINDA TEM QUE SE INSERIR 
            Mountains = Game1.sContent.Load<Texture2D>("Map1");
            Desert = Game1.sContent.Load<Texture2D>("Map2"); // AINDA TEM QUE SE INSERIR 

            ChaoPlataforma = new Plataforma(false, platSize, platPosition);
            plataforma.Add(ChaoPlataforma);

        }

        public void Update()
        {
            if (MapaMountain() == true)
            {
                selected = "Map1";
                a = new Duel(selected);
                a.Update();
                a.Draw();
            }
            if (MapaDesert() == true)
            {
                selected = "Map2";        /// É Preciso inserir o mapa do deserto
                a = new Duel(selected);
                a.Update();
                a.Draw();
            }
        }

        public void Draw()
        {

            Game1.sSpriteBatch.Draw(Museu, museuRec, Color.White);// AINDA IMAGEM POR FAZER

            a.Draw();
        }

        public bool MapaMountain()
        {
            if (c.attacks.hitbox.X <= mountainRec.X + Mountains.Width && c.attacks.hitbox.X + c.attacks.hitbox.Width >= mountainRec.X)
            {
                if (c.attacks.hitbox.Y < 600)
                {
                    return true;
                }
            }
            return false;
        }

        public bool MapaDesert()
        {
            if (c.attacks.hitbox.X <= desertRec.X + Desert.Width && c.attacks.hitbox.X + c.attacks.hitbox.Width >= desertRec.X)
            {
                if (c.attacks.hitbox.Y < 600)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
