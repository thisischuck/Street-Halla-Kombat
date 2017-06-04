using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace SHK
{
    class MainMenu
    {

        enum GameState { Menu, inGame, Paused }
        GameState gameState;// ainda por verificar se ta direito
        private Game b = new Game();


        #region PlayerStuff
        Character c;
        static Vector2 cPosition = new Vector2(1200, 100);
        static Vector2 cSize = new Vector2(400, 400);
        private List<Plataforma> plataforma = new List<Plataforma>();
        public AttackList attacks = new AttackList();

        // plataforma
        public Plataforma ChaoPlataforma;
        static Vector2 platSize = new Vector2(3500, 65);
        static Vector2 platPosition = new Vector2(1000, 100);

        #endregion

        #region MenuDisplay
        public Texture2D GUITexture1, GUITexture2, GUITexture3;
        private Rectangle GUIRect1, GUIRect2, GUIRect3;

        public delegate void ElementClicked();
        public event ElementClicked PressEvent; // chamada sempre que o enter é pressionado de forma a saber onde esta a presssionar
        #endregion

        public MainMenu()
        {
            #region MenuStuff
            GUITexture1 = Game1.sContent.Load<Texture2D>("menu");
            GUITexture2 = Game1.sContent.Load<Texture2D>("start");
            GUITexture3 = Game1.sContent.Load<Texture2D>("exit");
            GUIRect1 = new Rectangle(0, 0, GUITexture1.Width, GUITexture1.Height);
            GUIRect2 = new Rectangle(0, 0, GUITexture2.Width, GUITexture2.Height);
            GUIRect3 = new Rectangle(0, 0, GUITexture3.Width, GUITexture3.Height);
            #endregion

            ChaoPlataforma = new Plataforma(false, platSize, platPosition);
            plataforma.Add(ChaoPlataforma);
            c = new Character("Ryu-Final2", cPosition, cSize, 18, 29, 0, 2, SpriteEffects.None, false, plataforma, attacks);
            List<Projectil> inimigo = new List<Projectil>();
            c.SetInimigo(attacks, inimigo);

            CenterElement(Game1.mGraphics.PreferredBackBufferHeight, Game1.mGraphics.PreferredBackBufferWidth);
            SameDimensions(Game1.mGraphics.PreferredBackBufferWidth, Game1.mGraphics.PreferredBackBufferHeight);
            PressEvent += Events;


            //encontrar o determinado elemento e move-lo individualmente(em pixeis)

            MoveElement("start", -300, 285);
            MoveElement("exit", 300, 285);
        }

        public void Update()
        {
            switch(gameState)
            {
                case GameState.Menu:
                        Events();
                        c.Update();
                         break;
                case GameState.inGame:
                        break;
                case GameState.Paused:
                    break;
            }
        }

        public void Draw()
        {
           switch (gameState)
            {
                case GameState.Menu:

                    Game1.sSpriteBatch.Draw(GUITexture1, GUIRect1, Color.White);
                    Game1.sSpriteBatch.Draw(GUITexture2, GUIRect2, Color.White);
                    Game1.sSpriteBatch.Draw(GUITexture3, GUIRect3, Color.White);

                    c.Draw();
                        foreach (var plat in plataforma)
                        {
                            plat.Draw();
                        }
                    break;
                case GameState.inGame:
                        break;
                case GameState.Paused:
                        break;
            }
        }

        public void Events()
        {
            if( AtivaStart() == true)
            {
                // play the game
                gameState = GameState.inGame;
                
            }
            if( AtivaExit() == true)
            {
               //Game1.Quit();// nao funciona
            }
        }

        // centrar elementos do menu pelo tamanho da janela
        public void CenterElement(int height, int width)
        {
            GUIRect1 = new Rectangle((width / 2) - (this.GUITexture1.Width / 2), (height / 2) - (this.GUITexture1.Height / 2), this.GUITexture1.Width, this.GUITexture1.Height);
            GUIRect2 = new Rectangle((width / 2) - (this.GUITexture2.Width / 2), (height / 2) - (this.GUITexture2.Height / 2), this.GUITexture2.Width, this.GUITexture2.Height);
            GUIRect3 = new Rectangle((width / 2) - (this.GUITexture3.Width / 2), (height / 2) - (this.GUITexture3.Height / 2), this.GUITexture3.Width, this.GUITexture3.Height);
        }

        // mexer com a posiçao dos elementos do menu
        public void MoveElement(string name, int x, int y)
        {
            if (name == "start")
            {
                GUIRect2 = new Rectangle(GUIRect2.X += x, GUIRect2.Y += y, GUIRect2.Width, GUIRect2.Height);
            }
            else if (name == "exit")
            {
                GUIRect3 = new Rectangle(GUIRect3.X += x, GUIRect3.Y += y, GUIRect3.Width, GUIRect3.Height);
            }
            else GUIRect1 = new Rectangle(GUIRect1.X += x, GUIRect1.Y += y, GUIRect1.Width = y, GUIRect1.Height = x);
        }

        // torna a imagem com as mesmas dimensoes que a janela
        public void SameDimensions(int width, int height)
        {
            GUIRect1.X = width;
            GUIRect1.Y = height;
        }

       public bool AtivaStart()
        {
            if (c.attacks.hitbox.X <= GUIRect2.X + GUITexture2.Width && c.attacks.hitbox.X + c.attacks.hitbox.Width >= GUIRect2.X)
            {
                if (c.attacks.hitbox.Y < 600)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AtivaExit()
        {
            if (c.attacks.hitbox.X <= GUIRect3.X + GUITexture3.Width && c.attacks.hitbox.X + c.attacks.hitbox.Width >= GUIRect3.X)
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

