using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SHK
{
    class Duel 
    {

        string player1Choice;
        string player2Choice;

        public Character player1;
        public AttackList attacksPlayer1;
    
        static Vector2 charP = new Vector2(1200, 100);
        static Vector2 charS = new Vector2(300, 300);

        //---------------------------------------------------

        public Character player2;
        public AttackList attacksPlayer2;
        static Vector2 char2P = new Vector2(1208, 100);
        static Vector2 char2S = new Vector2(300, 300);

        //---------------------------------------------------

        private Map mapa;
        //Vector2 mapaSize = new Vector2(2343, 1470);
        Vector2 mapaSize = new Vector2(2650,1480);
        Vector2 mapaPosition = new Vector2(1250,700);


        GameTimer timer;
        TimerUI timUI;
            
        public Duel()
        {
            mapa = new Map(mapaSize,mapaPosition,"map1");


            timUI = new TimerUI(new Vector2(Game1.mGraphics.PreferredBackBufferWidth , Game1.mGraphics.PreferredBackBufferHeight / 2), Game1.mGraphics.PreferredBackBufferWidth, Game1.mGraphics.PreferredBackBufferHeight);
            timer = new GameTimer(180.0f);
            timer.Font = Game1.sContent.Load<SpriteFont>("Arial");
            timer.Position = new Vector2((Game1.mGraphics.PreferredBackBufferWidth / 2) - timer.Font.MeasureString(timer.Text).X / 2, 0);

            attacksPlayer1 = new AttackList();
            player1 = new Character("Ryu-Final2", charP, charS, 18, 29, 0, 1, SpriteEffects.None, false, mapa.ListaPlataformas, attacksPlayer1);
            

            attacksPlayer2 = new AttackList();
            player2 = new Character("Transcendent", char2P, char2S, 18, 29, 0, 2, SpriteEffects.None, false, mapa.ListaPlataformas, attacksPlayer2);

            player1.SetInimigo(attacksPlayer2);
            player2.SetInimigo(attacksPlayer1);
        }   

        public void Update()
        {
            player1.Update();
            player2.Update();

            if (player1.gotHit)
            {
                timUI.player1.setPlayerHealth(player1.playerHealth);
                timUI.player1.Update();
                player1.gotHit = false;
            }

            if(player2.gotHit)
            {
                timUI.player2.setPlayerHealth(player2.playerHealth);
                timUI.player2.Update();
                player2.gotHit = false;
            }


            timer.Update();

        }

        public void Draw()
        {
            mapa.Draw();
            timUI.Draw();
            timer.Draw();

            player1.Draw();
            player2.Draw();

            
            attacksPlayer1.Draw();
            attacksPlayer2.Draw();
        }
    }
}
