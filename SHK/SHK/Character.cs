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
        public Vector2 position;
        public Vector2 size;
        private float valorY;
        private float valorX;
        private float xSpeed;
        private float jumpSpeed;
        private float gravity;
        public bool isGrounded;
        public bool hasAirJump;
        private int airJumpCounter;
        private int airJumpDelay;           //em vez de utilizar um timer, podemos utilizar uma posição relativamente a si mesmo para tornar o airJump true e assim poder saltar
        private bool isAI;
        private bool animationPlay;
        private int playerNumber;
        public int playerHealth;

        private bool charPixelCollision;


        public Character(string imageName,Vector2 cposition, Vector2 csize,int row, int col, int padding, int player, SpriteEffects effect, bool ai) : base(imageName,cposition,csize,row,col,padding, effect)
        {
            mCurrentCharState = CharState.Idle;
            SetSpriteAnimation(0,0,0,1,2);
            position = cposition;
            size = csize;
            isGrounded = false;
            hasAirJump = true;
            isAI = ai;
            playerNumber = player;
            valorX = 0f;
            valorY = 0f;
            xSpeed = 10f;
            Speed = 10f;
            jumpSpeed = 50f;
            gravity = 2.5f;
            animationPlay = false;
            playerHealth = 100;
            airJumpCounter = 0;
            airJumpDelay = 25;
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

        public void Jump()
        {
            valorY = jumpSpeed;
            isGrounded = false;
            mCurrentCharState = CharState.Air;
            animationPlay = false;
        }

        public override void Update()
        {
            airJumpCounter++;

            if (!isAI)
            {
                if (playerNumber == 1)
                {


                    #region Movement + Animation

                    mPreviousCharState = mCurrentCharState;

                    if (Keyboard.GetState().IsKeyDown(Keys.W) && isGrounded)
                    {
                        Jump();
                    }

                    else if (Keyboard.GetState().IsKeyDown(Keys.W) && hasAirJump && airJumpCounter > airJumpDelay)
                    {
                        Jump();
                        hasAirJump = false;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.D))
                        {
                            valorX = xSpeed;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.A))
                        {
                            valorX = -xSpeed;
                        }
  
                        if (Keyboard.GetState().GetPressedKeys().Contains<Keys>(Keys.A) && Keyboard.GetState().GetPressedKeys().Contains<Keys>(Keys.D))
                        {
                            valorX = 0f;
                        }
                    }
                    else
                    {
                        valorX = 0f;
                    }

                    if (!isGrounded)
                    {
                        valorY -= gravity;
                    }

                    if (valorY == 0 && valorX == 0 && isGrounded)
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

                }

                if(playerNumber == 2)
                {


                    #region Movement + Animation

                    mPreviousCharState = mCurrentCharState;

                    if (Keyboard.GetState().IsKeyDown(Keys.Up) && isGrounded)
                    {
                        Jump();
                    }

                    else if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasAirJump && airJumpCounter > airJumpDelay)
                    {
                        Jump();
                        hasAirJump = false;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            valorX = xSpeed;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                            valorX = -xSpeed;
                        }

                        if (Keyboard.GetState().GetPressedKeys().Contains<Keys>(Keys.Left) && Keyboard.GetState().GetPressedKeys().Contains<Keys>(Keys.Right))
                        {
                            valorX = 0f;
                        }
                    }
                    else
                    {
                        valorX = 0f;
                    }

                    if (!isGrounded)
                    {
                        valorY -= gravity;
                    }

                    if (valorY == 0 && valorX == 0 && isGrounded)
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


                    
                }

                if (isGrounded)
                {
                    airJumpCounter = 0;
                    valorY = 0;
                    hasAirJump = true;

                }

                Velocity = (Vector2.UnitX * valorX) + (Vector2.UnitY * valorY);
                Console.WriteLine(airJumpCounter);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                if(playerHealth > 0)
                    playerHealth -= 1;
            }

            base.Update();
        }

        public void AnimationUpdate()
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
            AnimationUpdate();
            base.Draw();
        }
    }
}
