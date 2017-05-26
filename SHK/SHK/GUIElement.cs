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
    public class GUIElement
    {
        public Texture2D GUITexture, GUITexture1, GUITexture2;
        private Rectangle GUIRect, GUIRect1, GUIRect2;
        public string assetName = "start";

        public delegate void ElementClicked(string element);
        public event ElementClicked PressEvent; // chamada sempre que o enter é pressionado de forma a saber onde esta a presssionar

        public GUIElement()
        {
        }

        public void LoadContent(ContentManager content)
        {
            GUITexture  = content.Load<Texture2D>("menu");
            GUITexture1 = content.Load<Texture2D>("start");
            GUITexture2 = content.Load<Texture2D>("exit");
            GUIRect = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);
            GUIRect1 = new Rectangle(0, 0, GUITexture1.Width, GUITexture1.Height);
            GUIRect2 = new Rectangle(0, 0, GUITexture2.Width, GUITexture2.Height);
        }

        // verifica que elemento do menu é pressionado
        public void Update()
        {
            /*if (Keyboard.GetState().IsKeyDown(Keys.Down) && assetName == "start")
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
            }*/
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                PressEvent(assetName);
            }
           // Console.WriteLine(assetName);
        }

        public void Draw()
        {
           Game1.sSpriteBatch.Draw(GUITexture, GUIRect, Color.White);
           Game1.sSpriteBatch.Draw(GUITexture1, GUIRect1, Color.White);
           Game1.sSpriteBatch.Draw(GUITexture2, GUIRect2, Color.White);
        }

        // centrar elementos do menu pelo tamanho da janela
        public void CenterElement(int height, int width)
        {
            GUIRect  = new Rectangle((width / 2) - (this.GUITexture.Width / 2), (height / 2) - (this.GUITexture.Height / 2), this.GUITexture.Width, this.GUITexture.Height);
            GUIRect1 = new Rectangle((width / 2) - (this.GUITexture1.Width / 2), (height / 2) - (this.GUITexture1.Height / 2), this.GUITexture1.Width, this.GUITexture1.Height);
            GUIRect2 = new Rectangle((width / 2) - (this.GUITexture2.Width / 2), (height / 2) - (this.GUITexture2.Height / 2), this.GUITexture2.Width, this.GUITexture2.Height);
        }

        // mexer com a posiçao dos elementos do menu
        public void MoveElement(string name, int x, int y)
        {
            if(name == "start")
            {
                GUIRect1 = new Rectangle(GUIRect1.X += x, GUIRect1.Y += y, GUIRect1.Width, GUIRect1.Height);
            }
            else if(name == "exit")
            {
                GUIRect2 = new Rectangle(GUIRect2.X += x, GUIRect2.Y += y, GUIRect2.Width, GUIRect2.Height);
            }
            else GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, GUIRect.Width = y, GUIRect.Height = x);
        }
        // torna a imagem com as mesmas dimensoes que a janela
        public void SameDimensions(int x, int y)
        {
            // ainda por fazer..............................................................................................
        }
    }
}
