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
        private Vector2 position;
        private float valorY;
        private float valorX;
        private float xSpeed;
        private float jumpSpeed;
        private float gravity;
        private bool isGrounded;
        private bool isAI;
        private bool animationPlay;
        private int playerNumber;


        public Character(string imageName,Vector2 cposition, Vector2 csize,int row, int col, int padding, int player, SpriteEffects effect, bool ai) : base(imageName,cposition,csize,row,col,padding, effect)
        {
            mCurrentCharState = CharState.Idle;
            position = cposition;
            isGrounded = true;
            isAI = ai;
            playerNumber = player;
            valorX = 0f;
            valorY = 0f;
            xSpeed = 10f;
            Speed = 10f;
            jumpSpeed = 50f;
            gravity = 3f;
            animationPlay = false;
        }

        private enum CharState
        {
            Idle,
            WalkingFoward,
            WalkingBackwards,
            Dead,
            Air,
            Stunned,    
            MKick,
            MPunch,
            LPunch,
            LKick,
            HPunch,
            HKick,
            
        }

        private CharState mCurrentCharState;
        private CharState mPreviousCharState;



        public override void Update()
        {

            if (!isAI)
            {
                if (playerNumber == 1)
                {
                    Console.WriteLine(animationPlay);


                    #region Movement + Animation

                    mPreviousCharState = mCurrentCharState;

                    if (Keyboard.GetState().IsKeyDown(Keys.W) && isGrounded)
                    {
                        valorY = jumpSpeed;
                        isGrounded = false;
                        mCurrentCharState = CharState.Air;
                        animationPlay = false;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        valorX = xSpeed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.A))
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

                    if (valorY == 0 && valorX == 0)
                    {
                        mCurrentCharState = CharState.Idle;
                        animationPlay = false;
                    }

                    if (isGrounded)
                    {
                        if (valorX < 0)
                        {
                            mCurrentCharState = CharState.WalkingBackwards;
                            animationPlay = false;
                        }
                        else if (valorX > 0)
                        {
                            mCurrentCharState = CharState.WalkingFoward;
                            animationPlay = false;
                        }
                    }

                    #endregion


                    if (mPosition.Y < 100)
                    {
                        valorY = 0;
                        mPosition.Y = 100;
                        isGrounded = true;

                    }

                    Velocity = (Vector2.UnitX * valorX) + (Vector2.UnitY * valorY);
                }

                if(playerNumber == 2)
                {
                    Console.WriteLine(animationPlay);


                    #region Movement + Animation

                    mPreviousCharState = mCurrentCharState;

                    if (Keyboard.GetState().IsKeyDown(Keys.Up) && isGrounded)
                    {
                        valorY = jumpSpeed;
                        isGrounded = false;
                        mCurrentCharState = CharState.Air;
                        animationPlay = false;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        valorX = xSpeed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Left))
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

                    if (valorY == 0 && valorX == 0)
                    {
                        mCurrentCharState = CharState.Idle;
                        animationPlay = false;
                    }

                    if (isGrounded)
                    {
                        if (valorX < 0)
                        {
                            mCurrentCharState = CharState.WalkingBackwards;
                            animationPlay = false;
                        }
                        else if (valorX > 0)
                        {
                            mCurrentCharState = CharState.WalkingFoward;
                            animationPlay = false;
                        }
                    }

                    #endregion


                    if (mPosition.Y < 100)
                    {
                        valorY = 0;
                        mPosition.Y = 100;
                        isGrounded = true;

                    }

                    Velocity = (Vector2.UnitX * valorX) + (Vector2.UnitY * valorY);
                }


            }
            base.Update();
        }

        public void minuUpdate()
        {
                if (!animationPlay && mPreviousCharState != mCurrentCharState)
                {

                    switch (mCurrentCharState)
                    {
                        case CharState.Air:
                            SetSpriteAnimation(3, 0, 3, 1, 2);
                            break;
                        case CharState.Idle:
                            SetSpriteAnimation(0, 0, 0, 1, 2);
                            break;
                        case CharState.WalkingBackwards:
                            SetSpriteAnimation(2, 0, 2, 1, 2);
                            break;
                        case CharState.WalkingFoward:
                            SetSpriteAnimation(1, 0, 1, 1, 2);
                            break;

                    }

                    

                    animationPlay = true;

                }
        }

        public override void Draw()
        {
            minuUpdate();
            base.Draw();
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
        
    }
}
