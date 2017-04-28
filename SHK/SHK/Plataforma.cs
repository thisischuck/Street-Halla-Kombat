using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SHK
{
    class Plataforma : GameObject
    {
        bool isSoft;
        Vector2 mSize;
        Vector2 mPosition;

        private Character _player1;
        private Character _player2;

        private bool charPixelCollision;
        private bool charBoundCollision;

        public Plataforma(bool isSoft, Vector2 size, Vector2 position, Character player1, Character player2) : base("game_plat", position, size)
        {
            this.isSoft = isSoft;
            this.mSize = size;
            this.mPosition = position;

            _player1 = player1;
            _player2 = player2;
        }


        private void CollisionUpdate(Character player)
        {
            Vector2 pixelCollisionPosition = Vector2.Zero;

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
            }

        }

        public override void Update()
        {
            CollisionUpdate(_player1);
            CollisionUpdate(_player2);
        }
    }
}
