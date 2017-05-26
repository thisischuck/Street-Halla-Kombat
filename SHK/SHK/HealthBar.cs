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
    class HealthBar
    {
        private Vector2 position;
        Texture2D healthTexture;
        Rectangle healthRectangle;
        private SpriteEffects effect;
        private int playerHealth;

        public HealthBar(Vector2 hpPosition,Vector2 hpSize, SpriteEffects effect)
        {
            position = hpPosition;
            //containerPosition = new Vector2(70, 30);
            healthTexture = Game1.sContent.Load<Texture2D>("lifebar");
            healthRectangle = new Rectangle((int)hpPosition.X, (int)hpPosition.Y, (int)hpSize.X, (int)hpSize.Y);
            this.effect = effect;
        }

        public void setPlayerHealth(int aux)
        {
            this.playerHealth = aux;
        }

        public void Update()// mudar condiçao para quando o player é atacado
        {
            healthRectangle.Width = (int)(healthRectangle.Width * (playerHealth / 100f));
        }

        public void Draw()
        {
            Game1.sSpriteBatch.Draw(healthTexture, healthRectangle, Color.White);
        }
    }
}
