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
        private float tamanho;
        private Vector2 size;
        private float hpaux;
        public HealthBar(Vector2 hpPosition,Vector2 hpSize, SpriteEffects effect)
        {
            //this.size = size;
            position = hpPosition;
            //containerPosition = new Vector2(70, 30);
            healthTexture = Game1.sContent.Load<Texture2D>("lifebar");
            healthRectangle = new Rectangle((int)hpPosition.X, (int)hpPosition.Y, (int)hpSize.X, (int)hpSize.Y);
            hpaux = hpPosition.X;
            tamanho = hpSize.X;
            this.effect = effect;
        }

        public void setPlayerHealth(int aux)
        {
            this.playerHealth = aux;
        }

        public void Update()// mudar condiçao para quando o player é atacado
        {
            if (effect == SpriteEffects.None)
            {
                float cortou = (1 - playerHealth / 100f) * tamanho;
                healthRectangle.X = (int) Math.Round((hpaux + cortou)) ;
                
            }

            healthRectangle.Width = (int)(tamanho * (playerHealth / 100f));
            
        }

        public void Draw()
        {
            Rectangle destRect = Camera.ComputePixelRectangle(position, size);

            // define the rotation origin
            Vector2 org = new Vector2(size.X/2, size.Y / 2);

            // Draw the texture
            Game1.sSpriteBatch.Draw(healthTexture, healthRectangle, healthTexture.Bounds,Color.White, 0f, org, effect, 0);
        }
    }
}
