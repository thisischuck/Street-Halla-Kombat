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

            Speed = 10f;
            jumpSpeed = 50f;
            gravity = 3f;
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

        private CharState mCurrentCharState;

        public override void Update()
        {
            if (!isAI)
            {
                Console.WriteLine(Velocity);
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    VelocityDirection = Vector2.UnitX;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    VelocityDirection = Vector2.UnitX * -1;
                }
                else if (isGrounded)
                {
                    VelocityDirection = Vector2.Zero;
                    mCurrentCharState = CharState.idle;
                }
                

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && isGrounded)
                {
                    Velocity += Vector2.UnitY * jumpSpeed;
                    isGrounded = false;
                    mCurrentCharState = CharState.air;
                }
                if(!isGrounded)
                {
                    Velocity -= Vector2.UnitY * gravity;
                }

                if (mPosition.Y < 100)
                {
                    mPosition.Y = 100;
                    isGrounded = true;
                }
            }
            base.Update();
           
            /*
            if (!isAI)
            {
                Velocity = InputWrapper.ThumbSticks.Right;
                mSpeed = 10f;
                base.Update();
            }

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
                velocity.Y = Speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                velocity.Y = -Speed;
        }

        public override void Draw()
        {
            
            /*switch (mCurrentCharState)
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
            }*/

            base.Draw();

        }//falta por as animaçoes
    }
}
