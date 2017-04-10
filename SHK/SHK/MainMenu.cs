using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHK
{
    class MainMenu
    {
        enum GameState { Menu, inGame, Paused }
        GameState gameState;

        Duel a = new Duel();

        List<GUIElement> main = new List<GUIElement>();

        public MainMenu()
        {
            main.Add(new GUIElement("menu"));
            main.Add(new GUIElement("start"));
            main.Add(new GUIElement("exit"));
        }

        public void LoadContent(ContentManager content)
        {
            foreach( GUIElement element in main)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.mGraphics.PreferredBackBufferHeight, Game1.mGraphics.PreferredBackBufferWidth);
                element.clickEvent += onClick;
            }
            //encontrar o determinado elemento e move-lo individualmente( em pixeis)
            main.Find(x => x.AssetName == "start").MoveElement(0, -20);
            main.Find(x => x.AssetName == "exit").MoveElement(0, 20);
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

        public void onClick(string element)
        {
            if( element == "start")
            {
                // play the game
                gameState = GameState.inGame;

                a.Update();
                a.Draw();
            }
            if( element == "exit")
            {
                // exit the game
            }
        }
    }
}
