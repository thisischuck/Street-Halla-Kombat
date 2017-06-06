using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SHK
{
    class Duel 
    {
        enum GameState { Waiting, Playable }
        GameState gameState;

        static Vector2 charS = new Vector2(300, 300);

        public Character player1;
        public AttackList attacksPlayer1;
        static Vector2 charP = new Vector2(0, 0);

        //---------------------------------------------------

        public Character player2;
        public AttackList attacksPlayer2;
        static Vector2 char2P = new Vector2(0, 0);
        
        //---------------------------------------------------

        private Map mapa;
        private string nomeMapa;
        //Vector2 mapaSize = new Vector2(2343, 1470);
        Vector2 mapaSize = new Vector2(2650,1480);
        Vector2 mapaPosition = new Vector2(1250,700);
        public GameTime gameTime;

        //--------------------------------------------------COUNTER

        Texture2D fight;
        Rectangle fightRect;
        Vector2 fightPosition;



        GameTimer timer;
        TimerUI timUI;
            
        public Duel(string mapaEscolhido)
        {
            fight = Game1.sContent.Load<Texture2D>("Fight");
            fightRect = new Rectangle(fight.Bounds.X, fight.Bounds.Y, fight.Width + 416, fight.Height);
            fightPosition = new Vector2(Game1.mGraphics.PreferredBackBufferWidth / 2 - fight.Width / 2, Game1.mGraphics.PreferredBackBufferHeight / 2 - fight.Height / 2);

            this.nomeMapa = mapaEscolhido;
            mapa = new Map(mapaSize,mapaPosition,nomeMapa);


            timUI = new TimerUI(new Vector2(Game1.mGraphics.PreferredBackBufferWidth , Game1.mGraphics.PreferredBackBufferHeight / 2), Game1.mGraphics.PreferredBackBufferWidth, Game1.mGraphics.PreferredBackBufferHeight);
            /*timer = new GameTimer(180.0f);
            timer.Font = Game1.sContent.Load<SpriteFont>("Arial");
            timer.Position = new Vector2((Game1.mGraphics.PreferredBackBufferWidth / 2) - timer.Font.MeasureString(timer.Text).X / 2, 0);*/



            if (nomeMapa.Equals("map1"))
            {
                charP.X = 200;
                charP.Y = 1000;
                char2P.X = 2500;
                char2P.Y = 1000;
            }
            else if (nomeMapa.Equals("map2"))
            {
                
            }

            attacksPlayer1 = new AttackList();
            player1 = new Character("SpriteBrazilianRyu", charP, charS, 18, 32, 0, 1, SpriteEffects.None, false, mapa.ListaPlataformas, attacksPlayer1);
            

            attacksPlayer2 = new AttackList();
            player2 = new Character("SpriteBlackGayRyu", char2P, charS, 18, 32, 0, 2, SpriteEffects.FlipHorizontally, false, mapa.ListaPlataformas, attacksPlayer2);

            player1.SetInimigo(attacksPlayer2, player2.listHadouken);
            player2.SetInimigo(attacksPlayer1, player1.listHadouken);
        }   

        public void Update()
        {
            switch (gameState)
            {
                case GameState.Waiting:
                    Event();
                    break;
                case GameState.Playable:
                    timer.Update();
                    player1.Update();
                    player2.Update();
                    Check4Damage();
                    //timer.Update();

                    break;
            }
        }

        public void Draw()
        {
            //timer.Draw(Game1.sSpriteBatch);
            mapa.Draw();
            timUI.Draw();
            player1.Draw();
            player2.Draw();
            attacksPlayer1.Draw();
            attacksPlayer2.Draw();
            if (gameState == GameState.Waiting)
                Game1.sSpriteBatch.Draw(fight, fightPosition, fightRect, Color.White);
        }

        public void Check4Damage()
        {
            if (player1.gotHit)
            {
                timUI.player1.setPlayerHealth(player1.playerHealth);
                timUI.player1.Update();
                player1.gotHit = false;
            }

            if (player2.gotHit)
            {
                timUI.player2.setPlayerHealth(player2.playerHealth);
                timUI.player2.Update();
                player2.gotHit = false;
            }

            if (player1.playerHealth <= 0)
            {
                Console.WriteLine("Player2 Wins");
            }
            else if (player2.playerHealth <= 0)
            {
                Console.WriteLine("Player1 Wins");
            }
        }

        public void Event()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                gameState = GameState.Playable;
            }
        }
    }
}
