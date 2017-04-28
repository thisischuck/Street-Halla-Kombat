using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHK
{
    class GameTimer 
    {
        public SpriteFont Font;
        public float time;
        public bool started;
        public bool paused;
        public bool finished;
        public string Text;
        public Vector2 Position;
        public float deltatime;



        public GameTimer( float startTime)
        {
            time = startTime;
            started = false;
            paused = false;
            finished = false;
            Text = "";
        }

        public void Update(GameTime gameTime)
        {
            deltatime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (true) //started
            {
                if (true) //!paused
                {
                    if (time > 0)
                        time -= deltatime;
                    else
                        finished = true;
                }
            }
            Text = time.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color.White);
        }
    }
}
