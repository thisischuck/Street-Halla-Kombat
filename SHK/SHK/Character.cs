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
        private float fallingSpeedLimiter;
        public bool isGrounded;
        public bool hasAirJump;
        private int airJumpCounter;
        private int airJumpDelay;
        public bool isAttacking;
        private bool isAI;
        private bool animationPlay;
        private int playerNumber;
        public int playerHealth;
        private Keys jump, right, left, down, lPunch, mPunch, hPunch, lKick, mKick, hKick;
        private List<Plataforma> mapa;
        public AttackList attacks;


        public Character(string imageName,Vector2 cposition, Vector2 csize,int row, int col, int padding, int player, SpriteEffects effect, bool ai, List<Plataforma> mapa, AttackList attacks) : base(imageName,cposition,csize,row,col,padding, effect)
        {
            mCurrentCharState = CharState.Idle;
            SetSpriteAnimation(0,0,0,17,2);
            position = cposition;
            size = csize;
            isGrounded = false;
            hasAirJump = true;
            isAttacking = false;
            isAI = ai;
            playerNumber = player;
            valorX = 0f;
            valorY = 0f;
            xSpeed = 10f;
            jumpSpeed = 40f;
            gravity = 2f;
            fallingSpeedLimiter = -40f;
            animationPlay = false;
            playerHealth = 100;
            airJumpCounter = 0;
            airJumpDelay = 12;
            SetKeys();
            this.mapa = mapa;
            this.attacks = attacks;
        }

        private enum CharState
        {
            Idle,
            WalkingRight,
            WalkingLeft,
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


        private void SetKeys()
        {
            if (playerNumber == 1)
            {
                jump = Keys.W;
                right = Keys.D;
                left = Keys.A;
                down = Keys.S;
                lPunch = Keys.T;
                mPunch = Keys.Y;
                hPunch = Keys.U;
                lKick = Keys.G;
                mKick = Keys.H;
                hKick = Keys.J;
            }

            else if (playerNumber == 2)
            {
                jump = Keys.Up;
                right = Keys.Right;
                left = Keys.Left;
                down = Keys.Down;
                lPunch = Keys.NumPad4;
                mPunch = Keys.NumPad5;
                hPunch = Keys.NumPad6;
                lKick = Keys.NumPad1;
                mKick = Keys.NumPad2;
                hKick = Keys.NumPad3;
            }
        }

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
                #region Movement + Animation
           
                
                mPreviousCharState = mCurrentCharState;

                if (Keyboard.GetState().IsKeyDown(jump) && isGrounded)
                {
                    Jump();
                }
                else if (Keyboard.GetState().IsKeyDown(jump) && hasAirJump && (airJumpCounter > airJumpDelay))
                {
                    Jump();
                    hasAirJump = false;
                }

                if (Keyboard.GetState().IsKeyDown(left) || Keyboard.GetState().IsKeyDown(right))
                {
                    if (Keyboard.GetState().IsKeyDown(right))
                    {
                        valorX = xSpeed;
                    }

                    if (Keyboard.GetState().IsKeyDown(left))
                    {
                        valorX = -xSpeed;
                    }

                    if (Keyboard.GetState().GetPressedKeys().Contains<Keys>(left) && Keyboard.GetState().GetPressedKeys().Contains<Keys>(right))
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
                    if (valorY < fallingSpeedLimiter) valorY = fallingSpeedLimiter;
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
                        mCurrentCharState = CharState.WalkingLeft;
                        animationPlay = false;
                    }
                    else if (valorX > 0)
                    {
                        mCurrentCharState = CharState.WalkingRight;
                        animationPlay = false;
                    }
                }

                #endregion

                if (isGrounded)
                {
                    airJumpCounter = 0;
                    valorY = 0;
                    hasAirJump = true;
                }
            }

            #region Ataques

            if (!isAttacking)
            {
                if (Keyboard.GetState().IsKeyDown(lPunch))
                {
                    isAttacking = true;
                    attacks.LightPunch(mPosition);
                    isAttacking = false;
                }

                if (Keyboard.GetState().IsKeyDown(mPunch))
                {
                    isAttacking = true;
                    attacks.MediumPunch(mPosition);
                    isAttacking = false;
                }

                if (Keyboard.GetState().IsKeyDown(hPunch))
                {
                    isAttacking = true;
                    attacks.HeavyPunch(mPosition);
                    isAttacking = false;
                }

                if (Keyboard.GetState().IsKeyDown(lKick))
                {
                    isAttacking = true;
                    attacks.LightKick(mPosition);
                    isAttacking = false;
                }

                if (Keyboard.GetState().IsKeyDown(mKick))
                {
                    isAttacking = true;
                    attacks.MediumKick(mPosition);
                    isAttacking = false;
                }

                if (Keyboard.GetState().IsKeyDown(hKick))
                {
                    isAttacking = true;
                    attacks.HeavyKick(mPosition);
                    isAttacking = false;
                }
            }

            #endregion

            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                if(playerHealth > 0)
                    playerHealth -= 1;
            }

           

            Velocity = (Vector2.UnitX * valorX) + (Vector2.UnitY * valorY);
            Console.WriteLine(valorY);
            Collision();

            //position += Velocity;
            Console.WriteLine(hasAirJump);


            base.Update();
        }

        public void AnimationUpdate()
        {
                if (!animationPlay && mPreviousCharState != mCurrentCharState)
                {

                    switch (mCurrentCharState)
                    {
                        case CharState.Air:
                            SetSpriteAnimation(1, 0, 1, 9, 2);
                            break;
                        case CharState.Idle:
                            SetSpriteAnimation(0, 0, 0, 17, 2);
                            break;
                        case CharState.WalkingLeft:
                            this.SpriteEffects = SpriteEffects.FlipHorizontally;
                            SetSpriteAnimation(3, 0, 3, 6, 4);
                            break;
                        case CharState.WalkingRight:
                            this.SpriteEffects = SpriteEffects.None;
                            SetSpriteAnimation(3, 0, 3, 6, 4);
                            break;

                    }
                    animationPlay = true;
                }
        }

        public void Collision()
        {
            foreach (var plataforma in mapa)
            {
                if (mPosition.Y <= plataforma.Position.Y + plataforma.Size.Y / 2 && mPosition.Y > plataforma.Position.Y - plataforma.Size.Y / 2)
                {
                    if (mPosition.X > plataforma.Position.X - plataforma.Size.X / 2 && mPosition.X - size.X < plataforma.Position.X + plataforma.Size.X / 2)
                    {
                        isGrounded = true;
                        valorY = 0;
                        mPosition.Y = plataforma.Position.Y + plataforma.Size.Y / 2;
                            break;
                    }
                }
                else
                {
                    isGrounded = false;
                }
            }
        }

        public override void Draw()
        {
            AnimationUpdate();
            base.Draw();
        }
    }
}
