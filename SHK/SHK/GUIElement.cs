using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHK
{
    class GUIElement
    {
        private Texture2D GUITexture;
        private Rectangle GUIRect;
        public string assetName = "start";

        public delegate void ElementClicked(string element);
        public event ElementClicked PressEvent; // chamada sempre que o enter é pressionado de forma a saber onde esta a presssionar

        public GUIElement(string assetName)
        {
            this.assetName = assetName;
        }

        public void LoadContent(ContentManager content)
        {
            GUITexture = content.Load<Texture2D>(assetName);
            GUIRect = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);
        }

        // verifica que elemento do menu é pressionado
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && assetName == "start")
            {
                assetName = "exit";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && assetName == "exit")
            {
                assetName = "start";
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && assetName == "start")
            {
                assetName = "exit";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && assetName == "exit")
            {
                assetName = "start";
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                PressEvent(assetName);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           Game1.sSpriteBatch.Draw(GUITexture, GUIRect, Color.White);
        }

        // centrar elementos do menu pelo tamanho da janela
        public void CenterElement(int height, int width)
        {
            GUIRect = new Rectangle((width / 2) - (this.GUITexture.Width / 2), (height / 2) - (this.GUITexture.Height / 2), this.GUITexture.Width, this.GUITexture.Height);
        }

        // mexer com a posiçao dos elementos do menu
        public void MoveElement(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, GUIRect.Width, GUIRect.Height);
        }
    }
}
