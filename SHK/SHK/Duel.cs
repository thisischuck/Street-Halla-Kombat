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
        public HealthBar hpPlayer1;
        public AttackList attacksPlayer1;

        static Vector2 hpSize = new Vector2(700, 50);
        static Vector2 hpPosition1 = new Vector2(500, 500);
    
        static Vector2 charP = new Vector2(1200, 100);
        static Vector2 charS = new Vector2(300, 300);

        //---------------------------------------------------

        public Character player2;
        public HealthBar hpPlayer2;
        public AttackList attacksPlayer2;

        static Vector2 char2P = new Vector2(1208, 100);
        static Vector2 char2S = new Vector2(300, 300);

        static Vector2 hpPosition2 = new Vector2(0, 0);

        //---------------------------------------------------

        public List<Plataforma> ListaPlataformas = new List<Plataforma>();
          
        public Plataforma ChaoPlataforma;
        public Plataforma ChaoPlataforma2;
        static Vector2 platPosition = new Vector2(1000,100);
        static Vector2 platPosition2 = new Vector2(200, 200);
        public Plataforma ChaoPlataforma3;
        public Plataforma ChaoPlataforma4;
        static Vector2 platPosition3 = new Vector2(2000, 400);
        static Vector2 platPosition4 = new Vector2(1300, 700);
        static Vector2 platSize = new Vector2(510,65);

        // tamanho do TimerUI
        int TimerHeight = 221;
        int TimerWidth = 3676;

        GameTimer timer;
        TimerUI timUI;
            
        public Duel()
        {
            ChaoPlataforma = new Plataforma(false, platSize, platPosition);
            ChaoPlataforma2 = new Plataforma(false,platSize, platPosition2);
            ChaoPlataforma3 = new Plataforma(false, platSize, platPosition3);
            ChaoPlataforma4 = new Plataforma(false, platSize, platPosition4);
            


            timUI = new TimerUI(new Vector2(Game1.mGraphics.PreferredBackBufferWidth , Game1.mGraphics.PreferredBackBufferHeight / 2), Game1.mGraphics.PreferredBackBufferWidth, Game1.mGraphics.PreferredBackBufferHeight);
            timer = new GameTimer(180.0f);
            timer.Font = Game1.sContent.Load<SpriteFont>("Arial");
            timer.Position = new Vector2((Game1.mGraphics.PreferredBackBufferWidth / 2) - timer.Font.MeasureString(timer.Text).X / 2, 0);

            ListaPlataformas.Add(ChaoPlataforma);
            ListaPlataformas.Add(ChaoPlataforma2);
            ListaPlataformas.Add(ChaoPlataforma3);
            ListaPlataformas.Add(ChaoPlataforma4);

            attacksPlayer1 = new AttackList();
            player1 = new Character("Ryu-Final2", charP, charS, 18, 29, 0, 1, SpriteEffects.None, false, ListaPlataformas, attacksPlayer1);
            //hpPlayer1 = new HealthBar(player1, hpPosition1,hpSize, SpriteEffects.None);

            attacksPlayer2 = new AttackList();
            player2 = new Character("Transcendent", char2P, char2S, 18, 29, 0, 2, SpriteEffects.None, false, ListaPlataformas, attacksPlayer2);
            hpPlayer2 = new HealthBar(player2, hpPosition2,hpSize, SpriteEffects.FlipHorizontally);

            player1.SetInimigo(attacksPlayer2);
            player2.SetInimigo(attacksPlayer1);
        }   

        public void Update()
        {
            timer.Update();
            player1.Update();
            //hpPlayer1.Update();

            player2.Update();
            //hpPlayer2.Update();
        }

        public void Draw()
        {
            
            timUI.Draw();
            timer.Draw();
            //hpPlayer1.Draw();
            // hpPlayer2.Draw();
            player1.Draw();

            player2.Draw();
            foreach (var plataforma in ListaPlataformas)
            {
                plataforma.Draw();
            }
            attacksPlayer1.Draw();
            attacksPlayer2.Draw();
        }
    }
}
