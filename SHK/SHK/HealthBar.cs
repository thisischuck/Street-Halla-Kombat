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
        int playerHealth= 100;

        public HealthBar(ContentManager content)
        {
            position = new Vector2(50, 30);
            //containerPosition = new Vector2(70, 30);
            LoadContent(content);
        }


        private void LoadContent(ContentManager content)
        {
            lifebar = content.Load<Texture2D>("healthbar");
            rectangle = new Rectangle(0, 0, lifebar.Width, lifebar.Height);
            //container = content.Load<Texture2D>(""); //parte fixa da barra de vida
        }

        public void Update()// mudar condiçao para quando o player é atacado
        {
            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                rectangle.Width -= 1;
                playerHealth -= 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(container, containerPosition, Color.White);
            spriteBatch.Draw(lifebar, position, rectangle, Color.White);
        }
    }
}
