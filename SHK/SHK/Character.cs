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
        float valorY;
        float valorX;
        float xSpeed;
        float jumpSpeed;
        float gravity;
        bool isGrounded;
        bool isAI;


        public Character(string imageName,Vector2 cposition, Vector2 csize,int row, int col, int padding, SpriteEffects effect, bool ai) : base(imageName,cposition,csize,row,col,padding, effect)
        {
            mCurrentCharState = CharState.idle;
            position = cposition;
            isGrounded = true;
            isAI = ai;

            valorX = 0f;
            valorY = 0f;
            xSpeed = 10f;
            Speed = 10f;
            jumpSpeed = 50f;
            gravity = 3f;
        }

        private enum CharState
        {
            idle,
            walkingFoward,
            walkingBackwards,
            dead,
            air,
            stunned,    
            mKick,
            mPunch,
            lPunch,
            lKick,
            hPunch,
            hKick,
            
        }

        private CharState mCurrentCharState;



        public override void Update()
        {

            if (!isAI)
            {
                minuUpdate();
                Console.WriteLine(SpriteCurrentRow + " " + SpriteCurrentColumn + " " + McurrentTick);

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && isGrounded)
                {
                    valorY = jumpSpeed;
                    isGrounded = false;
                    mCurrentCharState = CharState.air;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right) )
                {
                    valorX = xSpeed;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left) )
                {
                    valorX = -xSpeed;
                }
                else
                {
                    valorX = 0f;

                }

                if (!isGrounded)
                {
                    valorY -= gravity;
                }

                if (mPosition.Y < 100)
                {
                    valorY = 0;
                    mPosition.Y = 100;
                    isGrounded = true;

                }

                if (valorY == 0 && valorX == 0 && isGrounded)
                {
                    mCurrentCharState = CharState.idle;
                }


                if(isGrounded && valorX > 0)
                {
                    mCurrentCharState = CharState.walkingFoward;
                }
                else if (isGrounded && valorX < 0)
                {
                    mCurrentCharState = CharState.walkingBackwards;
                }
                /* if (isGrounded) //para parar a queda mesmo se estiver a movimentar-se
                {
                    valorY = 0f;
                }
                */
                Velocity = (Vector2.UnitX * valorX) + (Vector2.UnitY * valorY);
                //mVelocityDir = (Vector2.UnitX * valorX);              esta variável é inutil existir neste código

                
            }
            base.Update();
        }

        public void minuUpdate()
        {
            if(mCurrentCharState == CharState.air)
                SetSpriteAnimation(3, 0, 3, 1, 2);
            else if(mCurrentCharState == CharState.walkingFoward)
                SetSpriteAnimation(1, 0, 1, 1, 2);
            else if (mCurrentCharState == CharState.walkingBackwards)
                SetSpriteAnimation(2, 0, 2, 1, 2);
            else if (mCurrentCharState == CharState.idle)
                SetSpriteAnimation(0, 0, 0, 1, 2);
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
        
        /*private void Move()
        {
            foreach (var sprite in char)
            {
                if (sprite == this)
                    continue;

                if (this.velocity.X > 0 && this.IsTouchingLeft(sprite) || this.velocity.X < 0 && this.IsTouchingRight(sprite))
                    this.velocity.X = 0;
                if (this.velocity.Y > 0 && this.IsTouchingTop(sprite) || this.velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.velocity.Y = 0;
            }
            position += velocity;

            velocity = Vector2.Zero; }
            */

        public override void Draw()
        {
            base.Draw();
        }
        
    }
}
