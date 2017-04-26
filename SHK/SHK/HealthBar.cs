using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;// retirar posteriormente
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHK
{
    class HealthBar
    {
        private Texture2D lifebar, container;
        private Vector2 position, containerPosition;
        Rectangle rectangle;
        Character player;

        public HealthBar(ContentManager content, Character player, Vector2 hpPosition, SpriteEffects effect)
        {
            position = hpPosition;
            //containerPosition = new Vector2(70, 30);
            LoadContent(content);
            this.player = player;
        }


        private void LoadContent(ContentManager content)
        {
            lifebar = content.Load<Texture2D>("healthbar");
            rectangle = new Rectangle(0, 0, lifebar.Width, lifebar.Height);
            //container = content.Load<Texture2D>(""); //parte fixa da barra de vida
        }

        public void Update()// mudar condiçao para quando o player é atacado
        {
            rectangle.Width = lifebar.Width * (player.playerHealth / 100);
            Console.WriteLine(rectangle.Width);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(container, containerPosition, Color.White);
            spriteBatch.Draw(lifebar, position, rectangle, Color.White);
        }
    }
}
