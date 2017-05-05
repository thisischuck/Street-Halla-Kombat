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
        Duel a;

        List<GUIElement> main = new List<GUIElement>();

        public MainMenu()
        {
            main.Add(new GUIElement("menu"));
            main.Add(new GUIElement("start"));
            main.Add(new GUIElement("exit"));
        }

        public void LoadContent(ContentManager content)
        {

            a = new Duel();
            foreach ( GUIElement element in main)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.mGraphics.PreferredBackBufferHeight, Game1.mGraphics.PreferredBackBufferWidth);
                element.PressEvent += onPress;
            }
            //encontrar o determinado elemento e move-lo individualmente(em pixeis)
            main.Find(x => x.assetName == "start").MoveElement(0, -20);
            main.Find(x => x.assetName == "exit").MoveElement(0, 20);
        }

        public void Update()
        {
            switch(gameState)
            {
                case GameState.Menu:
                    foreach (GUIElement element in main)
                    {
                        element.Update();
                    }
                    break;
                case GameState.inGame:
                    break;
                case GameState.Paused:
                    break;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    foreach (GUIElement element in main)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.inGame:
                    break;
                case GameState.Paused:
                    break;
            }
        }

        public void onPress(string element)
        {
            if( element == "start")
            {
                // play the game
                gameState = GameState.inGame;
                Console.WriteLine("IT WORKED.");
                
            }
            if( element == "exit")
            {
              b.Exit();//verificar se ta direito
            }
        }
    }
}
