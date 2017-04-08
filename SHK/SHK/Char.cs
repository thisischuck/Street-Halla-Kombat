using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SHK
{
    public class Character : SpritePrimitive
    {
        Vector2 position;
        Vector2 velocity;
        Vector2 speed;
        bool hasJumped;

        public Character(Vector2 newPosition, string imageName,Vector2 cposition, Vector2 csize,int row, int col, int padding) : base(imageName,cposition,csize,row,col,padding)
        {
            mCurrentCharState = CharState.idle;
            position = newPosition;
        }

        private enum CharState
        {
            idle,
            dead,
            air,
            stunned,    
            mKick,
            mPunch,
            lPunch,
            lKick,
            hPunch,
            hKick
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = 3f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -3f;
            }
            else velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;
            }

            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i; // aumenta o valor do float para cair mais rapido
            }

            if (position.Y + mImage.Height >= 450)
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }
/*
            Move();

            foreach(var sprite in char)
            {
                if (sprite == this)
                    continue;

                if (this.velocity.X > 0 && this.IsTouchingLeft(sprite) || this.velocity.X < 0 && this.IsTouchingRight(sprite))
                    this.velocity.X = 0;
                if (this.velocity.Y > 0 && this.IsTouchingTop(sprite) || this.velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.velocity.Y = 0;
            }
            position += velocity;

            velocity = Vector2.Zero;*/
        }
        
        #region Colisoes
        /*protected bool IsTouchingLeft(Char character)
        {
            return this.Rectangule.Right + this.velocity.X > character.Rectangule.Left &&
                this.Rectangule.Left < character.Rectangule.Left &&
                this.Rectangule.Bottom > character.Rectangule.Top &&
                this.Rectangule.Top < character.Rectangule.Bottom;
        }

        protected bool IsTouchingRight(Char character)
        {
            return this.Rectangule.Left + this.velocity.X < character.Rectangule.Right &&
                this.Rectangule.Left > character.Rectangule.Left &&
                this.Rectangule.Bottom > character.Rectangule.Top &&
                this.Rectangule.Top < character.Rectangule.Bottom;
        }

        protected bool IsTouchingTop(Char character)
        {
            return this.Rectangule.Bottom + this.velocity.X > character.Rectangule.Top &&
                this.Rectangule.Top < character.Rectangule.Top &&
                this.Rectangule.Right > character.Rectangule.Left &&
                this.Rectangule.Left < character.Rectangule.Bottom;
        }

        protected bool IsTouchingBottom(Char character)
        {
            return this.Rectangule.Top + this.velocity.X < character.Rectangule.Bottom &&
                this.Rectangule.Bottom > character.Rectangule.Bottom &&
                this.Rectangule.Right > character.Rectangule.Left &&
                this.Rectangule.Left < character.Rectangule.Right;
        }*/
        #endregion
        
        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity.X = Speed;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                velocity.Y = Speed;
        }

        private CharState mCurrentCharState;

        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        {
            

            switch (mCurrentCharState)
            {
                case CharState.idle:

                    break;

                case CharState.dead:

                    break;

                case CharState.air:

                    break;

                case CharState.stunned:

                    break;

                case CharState.mKick:

                    break;

                case CharState.lKick:

                    break;

                case CharState.hKick:

                    break;

                case CharState.mPunch:

                    break;

                case CharState.lPunch:

                    break;

                case CharState.hPunch:

                    break;
            }

            base.Draw();

        }//falta por as animaçoes
    }
}
