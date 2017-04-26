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

        public Plataforma(bool isSoft, Vector2 size, Vector2 position, Character player1, Character player2) : base("game_plat", position, size)
        {
            this.isSoft = isSoft;
            this.mSize = size;
            this.mPosition = position;

            _player1 = player1;
            _player2 = player2;
        }


        private void CollisionUpdate()
        {
            Vector2 pixelCollisionPosition = Vector2.Zero;

            charPixelCollision = PrimitivesTouches(_player1);
            if (charPixelCollision)
            {
                charPixelCollision = PixelTouches(mFlower, out pixelCollisionPosition);
                if (mHeroPixelCollision)
                    mHeroTarget.Position = pixelCollisionPosition;
            }
        }
    }
}
