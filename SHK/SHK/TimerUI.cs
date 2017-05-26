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

        public TimerUI(Vector2 timerSize, int width, int height)
        {
            //TimerTexture = Game1.sContent.Load<Texture2D>("timerContainer");
            //TimerRect = new Rectangle(0, 0, TimerTexture.Width, TimerTexture.Height);
            //221
            TimerTexture = Game1.sContent.Load<Texture2D>("timerContainer");
            TimerRect = new Rectangle(0,0, (int)(timerSize.X ), 115);

            


        }
        public void Draw()
        {
            Game1.sSpriteBatch.Draw(TimerTexture, TimerRect, Color.White);
        }
    }
}

