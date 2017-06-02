using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    class TimerUI
    {
        Texture2D TimerTexture;
        Rectangle TimerRect;
        public HealthBar player1;
        public HealthBar player2;
        private Vector2 hSize = new Vector2(553, 40);
        private Vector2 hp1Position = new Vector2(17,68);
        private Vector2 hp2Position = new Vector2(710, 68);

        public TimerUI(Vector2 timerSize, int width, int height)
        {
            //TimerTexture = Game1.sContent.Load<Texture2D>("timerContainer");
            //TimerRect = new Rectangle(0, 0, TimerTexture.Width, TimerTexture.Height);
            //221
            TimerTexture = Game1.sContent.Load<Texture2D>("timerContainer");
            TimerRect = new Rectangle(0,0, (int)(timerSize.X ), 115);
        }

            player1 = new HealthBar(hp1Position,hSize, SpriteEffects.None);
            player2 = new HealthBar(hp2Position, hSize, SpriteEffects.FlipHorizontally);

        }

        public void Draw()
        {
            
           
            player1.Draw();
            player2.Draw();
            Game1.sSpriteBatch.Draw(TimerTexture, TimerRect, Color.White);
        }
    }
}

