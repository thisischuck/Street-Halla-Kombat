using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;// retirar posteriormente
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHK
{
    class HealthBar : SpritePrimitive
    {
        private Vector2 position;
        Rectangle rectangle;
        Character player;
        private int width;
        private int height;
        private SpriteEffects effect;

        public HealthBar(Character player, Vector2 hpPosition,Vector2 hpSize, SpriteEffects effect) :base("healthbar",hpPosition, hpSize, 1,1,0,effect)
        {
            position = hpPosition;
            //containerPosition = new Vector2(70, 30);
            this.player = player;
            height = SpriteImageHeight;
            width = SpriteImageWidth;
            rectangle = new Rectangle(0, 0, width, height);
            this.effect = effect;
        }


        public void Update()// mudar condiçao para quando o player é atacado
        {
            rectangle.Width = (int)((float)(width) * ((float)(player.playerHealth) / 100f));
        }

        public override void Draw()
        {
            Rectangle destRect = Camera.ComputePixelRectangle(mPosition, mSize);
            Rectangle srcRect = new Rectangle((int)position.X, (int)position.Y, width, height);
            Vector2 org = new Vector2(width / 2,height / 2);

            //Game1.sSpriteBatch.Draw(mImage,destRect,srcRect, Color.White,0f,org,effect,0);

            Game1.sSpriteBatch.Draw(mImage,position,rectangle);
        }
    }
}
