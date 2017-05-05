using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHK
{
    class TimerUI : GameObject
    {
        public Vector2 position;
        Rectangle rectangle;
        private int width;
        private int height;

        public TimerUI(Vector2 timerPosition, Vector2 timerSize) :base("timerContainer",timerPosition, timerSize)
        {
            position = timerPosition;
            height = SpriteImageHeight;
            width = SpriteImageWidth;
            rectangle = new Rectangle(0, 0, width, height);

        }
        public override void Draw()
        {
            Rectangle destRect = Camera.ComputePixelRectangle(mPosition, mSize);
            Rectangle srcRect = new Rectangle((int)position.X, (int)position.Y, width, height);
            Vector2 org = new Vector2(width / 2, height / 2);
            Game1.sSpriteBatch.Draw(mImage, position, rectangle, Color.White);
        }

        public void DownScale(int windowWidth, int windowHeight)
        {
            height = windowHeight / 2;
            windowWidth = windowWidth / 2;
        }
    }
}

