using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    public class Plataforma : GameObject
    {
        bool isSoft;
        public Vector2 Size;
        public Vector2 Position;

        private Character _player1;
        private Character _player2;

        private bool charPixelCollision;
        private bool charBoundCollision;

        private Rectangle a;
        private Texture2D a_text;

        public Plataforma(bool isSoft, Vector2 size, Vector2 position, Character player1, Character player2) : base("game_plat", position, size)
        {
            this.isSoft = isSoft;
            this.Size = size;
            this.Position = position;

            _player1 = player1;
            _player2 = player2;

            a = Camera.ComputePixelRectangle(Position, Size);
            a_text = new Texture2D(Game1.mGraphics.GraphicsDevice, a.Width, a.Height);

            Color[] data = new Color[a.Width * a.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            a_text.SetData(data);
        }

        private void CollisionUpdate(Character player)
        {
                /*Vector2 pixelCollisionPosition = Vector2.Zero;

                charBoundCollision = PrimitivesTouches(player);
                charPixelCollision = charBoundCollision;
                if (charBoundCollision)
                {
                    charPixelCollision = PixelTouches(player, out pixelCollisionPosition);
                    if (charPixelCollision)
                    {
                        player.mPosition.Y = pixelCollisionPosition.Y;
                        player.isGrounded = true;
                    }
                }*/


            if (player.mPosition.Y < this.Position.Y + Size.Y/2 && player.mPosition.Y > this.Position.Y - Size.Y/2)
            {
                if (player.mPosition.X > this.Position.X - Size.X/2 && player.mPosition.X - player.size.X < this.Position.X + Size.X/2)
                {
                    player.isGrounded = true;
                    player.mPosition.Y = this.mPosition.Y + Size.Y / 2;
                }
            }
            else
            {
                player.isGrounded = false;
            }
            
        }

        public override void Update()
        {
            CollisionUpdate(_player1);
            CollisionUpdate(_player2);
        }

        public override void Draw()
        {
            base.Draw();
            Game1.sSpriteBatch.Draw(a_text,a,Color.Black);
            
        }
    }
}
