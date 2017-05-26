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
        GUIElement element;

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



        public MainMenu()
        {
            ChaoPlataforma = new Plataforma(false, platSize, platPosition);
            plataforma.Add(ChaoPlataforma);
            a = new Duel();
            c = new Character("Ryu-Final2", cPosition, cSize, 18, 29, 0, 2, SpriteEffects.None, false, plataforma, attacks);
            c.SetInimigo(attacks);


            element = new GUIElement();
            element.LoadContent(Game1.sContent);
            element.CenterElement(Game1.mGraphics.PreferredBackBufferHeight, Game1.mGraphics.PreferredBackBufferWidth);
            element.PressEvent += onPress;
            //encontrar o determinado elemento e move-lo individualmente(em pixeis)
            element.MoveElement("start", -300, 285);
            element.MoveElement("exit", 300, 285);
        }

        public void Update()
        {
            switch(gameState)
            {
                case GameState.Menu:
                        element.Update();
                        c.Update();
                         break;
                case GameState.inGame:
                        a.Update();
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
                        element.Draw();
                        c.Draw();
                        foreach (var plat in plataforma)
                        {
                            plat.Draw();
                        }
                    break;
                case GameState.inGame:
                        a.Draw();
                        break;
                case GameState.Paused:
                        break;
            }
        }

        public void onPress(string buttonName)
        {
            if( buttonName == "start")
            {
                // play the game
                gameState = GameState.inGame;
                
            }
            if( buttonName == "exit")
            {
              b.Exit();//verificar se ta direito
            }
        }
    }
}
