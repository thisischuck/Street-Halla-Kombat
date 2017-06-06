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
    class MainMenu : Game
    {

        enum GameState { Menu, MapSelect ,inGame, Paused }
        GameState gameState;// ainda por verificar se ta direito
        Duel a;
        private bool loaded;
        Game game;
        #region PlayerStuff
        Character c;
        static Vector2 cPosition = new Vector2(1200, 100);
        static Vector2 cSize = new Vector2(400, 400);
        private List<Plataforma> plataforma = new List<Plataforma>();

        private Texture2D menu, start_text, exit_text, museu;
        private Rectangle menuRect, startRect, exitRect, museuRect;

        // plataforma
        public Plataforma ChaoPlataforma;
        static Vector2 platSize = new Vector2(3500, 65);
        static Vector2 platPosition = new Vector2(1000, 50);

        #endregion

        #region MenuDisplay
        public delegate void ElementClicked();
        public event ElementClicked PressEvent; // chamada sempre que o enter é pressionado de forma a saber onde esta a presssionar
        #endregion

        public MainMenu()
        {
            #region MenuStuff
            menu = Game1.sContent.Load<Texture2D>("Menu");
            menuRect = new Rectangle(menu.Bounds.X,menu.Bounds.Y,menu.Width + 416,menu.Height + 240);

            Vector2 startSize = new Vector2(100,200);
            Vector2 startPosition = new Vector2(300,450);

            startRect = new Rectangle((int) startPosition.X, (int) startPosition.Y, (int) startSize.X,(int) startSize.Y);

            Vector2 exitSize = new Vector2(75, 200);
            Vector2 exitPosition = new Vector2(1200, 475);

            exitRect = new Rectangle((int)exitPosition.X, (int)exitPosition.Y, (int)exitSize.X, (int)exitSize.Y);

            #endregion

            #region PlayerStuff

            ChaoPlataforma = new Plataforma(false, platSize, platPosition);
            plataforma.Add(ChaoPlataforma);
            c = new Character("SpriteBrazilianRyu", cPosition, cSize, plataforma);

            #endregion

            #region MapSelect

            museu = Game1.sContent.Load<Texture2D>("MapSelection");
            museuRect = new Rectangle(museu.Bounds.X, museu.Bounds.Y, museu.Width, museu.Height);

            #endregion

            PressEvent += Events;
        }

        public void Update()
        {
            switch(gameState)
            {
                case GameState.Menu:
                        Events();
                        c.Update();
                         break;
                case GameState.MapSelect:
                    Events();
                    c.Update();
                    break;
                case GameState.inGame:
                        a.Update();
                        break;
                case GameState.Paused:
                    break;
            }
            if(loaded)
            {
                if(a.matchEnd == true)
                {

                }
            }
        }

        public void Draw()
        {
           switch (gameState)
            {
                case GameState.Menu:
                    Game1.sSpriteBatch.Draw(menu,menuRect,Color.White);

                    c.Draw();
                    
                   break;
                case GameState.MapSelect:
                   Game1.sSpriteBatch.Draw(museu, museuRect, Color.White);
                   c.Draw();
                        
                    break;
                case GameState.inGame:
                        a.Draw();
                        break;
                case GameState.Paused:
                    break;
            }
        }

        public void Events()
        {
            if(gameState == GameState.Menu)
            { 
                if( AtivaStart())
                {
                 // play the game
                Console.WriteLine("Switch");
                gameState = GameState.MapSelect;
                c.mSize = new Vector2(600, 600);
                platPosition = new Vector2(700, 50);
                }
                if( AtivaExit())
                {   
                 //game.Exit(); nao funciona
                }
            }
            else if (gameState == GameState.MapSelect)
            {
                if (MapaMountain())
                {
                    loaded = true;
                     a = new Duel("map1");
                    gameState = GameState.inGame;
                }
                else if (MapaDesert())
                {
                    loaded = true;
                    a = new Duel("map2");
                    gameState = GameState.inGame;
                }
            }
        }

        #region Choose

        public bool AtivaStart()
        {
            if (c.attacks.hitbox.X <= startRect.X + startRect.Width && c.attacks.hitbox.X + c.attacks.hitbox.Width >= startRect.X)
            {
                if (c.attacks.hitbox.Y + c.attacks.hitbox.Height >= startRect.Y && c.attacks.hitbox.Y <= startRect.Y + startRect.Height)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AtivaExit()
        {
            if (c.attacks.hitbox.X <= exitRect.X + exitRect.Width && c.attacks.hitbox.X + c.attacks.hitbox.Width >= exitRect.X)
            {
                if (c.attacks.hitbox.Y + c.attacks.hitbox.Height >= exitRect.Y && c.attacks.hitbox.Y <= exitRect.Y + exitRect.Height)
                {
                    return true;
                }
            }
            return false;
        }

        public bool MapaMountain()
        {
            if (c.attacks.hitbox.X <= 647 && c.attacks.hitbox.X + c.attacks.hitbox.Width >= 104)
            {
                if (c.attacks.hitbox.Y <= 508)
                {
                    return true;
                }
            }
            return false;
        }

        public bool MapaDesert()
        {
            if (c.attacks.hitbox.X <= 1254 && c.attacks.hitbox.X + c.attacks.hitbox.Width >= 713)
            {
                if (c.attacks.hitbox.Y <= 508)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}

