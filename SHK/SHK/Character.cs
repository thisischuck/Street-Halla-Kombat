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
        private int airJumpCounter, comboCounter;
        private int airJumpDelay;
        public bool isAttacking;
        private bool isAI;
        private bool animationPlay;
        private int playerNumber;
        public int playerHealth;
        private Keys jump, right, left, down, lPunch, mPunch, hPunch, lKick, mKick, hKick;
        private List<Plataforma> mapa;
        public AttackList attacks;
        public Queue<Keys> movementKeyHistory;
        public bool isRightDown, isLeftDown, isDownDown;
        public bool hasHadouken;
        private Rectangle hurtbox;
        private Texture2D a_text;

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
            movementKeyHistory = new Queue<Keys>();
            isRightDown = false;
            isLeftDown = false;
            isDownDown = false;
            hasHadouken = false;
            comboCounter = 0;

            
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
            MKickAir,
            MPunchAir,
            LPunchAir,
            LKickAir,
            HPunchAir,
            HKickAir,
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
            Vector2 a = new Vector2(mPosition.X - mSize.X/2 - 10, mPosition.Y + mSize.Y/2 - 40);
            //Vector2 a = new Vector2(mPosition.X, mPosition.Y);
            Vector2 b = new Vector2(mSize.X/2 - 50, mSize.Y - mSize.Y/3);
            hurtbox = Camera.ComputePixelRectangle(a, b);
            a_text = new Texture2D(Game1.mGraphics.GraphicsDevice, hurtbox.Width, hurtbox.Height);

            Color[] data = new Color[hurtbox.Width * hurtbox.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            a_text.SetData(data);

            airJumpCounter++;
            comboCounter++;

            if (comboCounter > 50)
            {
                hasHadouken = false;
                comboCounter = 0;
            }
            if (movementKeyHistory.Count > 3 && hasHadouken == false)
                hasHadouken = CheckHadouken();

            #region QueueKeyHistory

            if (Keyboard.GetState().IsKeyDown(right))
                isRightDown = true;
            if (isRightDown == true && Keyboard.GetState().IsKeyUp(right))
            {
                isRightDown = false;
                movementKeyHistory.Enqueue(right);
            }


            if (Keyboard.GetState().IsKeyDown(left))
                isLeftDown = true;
            if (isLeftDown == true && Keyboard.GetState().IsKeyUp(left))
            {
                isLeftDown = false;
                movementKeyHistory.Enqueue(left);
            }


            if (Keyboard.GetState().IsKeyDown(down))
                isDownDown = true;
            if (isDownDown == true && Keyboard.GetState().IsKeyUp(down))
            {
                isDownDown = false;
                movementKeyHistory.Enqueue(down);
            }

            #endregion

            if (!isAI)
            {
                if (!isAttacking)
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

                    if (valorY == 0 && valorX == 0 && isGrounded && !isAttacking)
                    {
                        mCurrentCharState = CharState.Idle;
                        animationPlay = false;
                    }

                    if (isGrounded && !isAttacking)
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
                }

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
                if (isGrounded)
                {
                    if (Keyboard.GetState().IsKeyDown(lPunch))
                    {
                        valorX = 0;
                        isAttacking = true;
                        attacks.LightPunch(mPosition, SpriteEffects);
                        mCurrentCharState = CharState.LPunch;
                    }

                    if (Keyboard.GetState().IsKeyDown(mPunch))
                    {
                        valorX = 0;
                        isAttacking = true;
                        attacks.MediumPunch(mPosition, SpriteEffects);
                        mCurrentCharState = CharState.MPunch;
                    }

                    if (Keyboard.GetState().IsKeyDown(hPunch))
                    {
                        valorX = 0;
                        isAttacking = true;
                        attacks.HeavyPunch(mPosition, SpriteEffects);
                        mCurrentCharState = CharState.HPunch;
                    }

                    if (Keyboard.GetState().IsKeyDown(lKick))
                    {
                        valorX = 0;
                        isAttacking = true;
                        attacks.LightKick(mPosition, SpriteEffects);
                        mCurrentCharState = CharState.LKick;
                    }

                    if (Keyboard.GetState().IsKeyDown(mKick))
                    {
                        valorX = 0;
                        isAttacking = true;
                        attacks.MediumKick(mPosition, SpriteEffects);
                        mCurrentCharState = CharState.MKick;
                    }

                    if (Keyboard.GetState().IsKeyDown(hKick))
                    {
                        valorX = 0;
                        isAttacking = true;
                        attacks.HeavyKick(mPosition, SpriteEffects);
                        mCurrentCharState = CharState.HKick;
                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(lPunch))
                    {
                        isAttacking = true;
                        attacks.LightPunchAir(mPosition, SpriteEffects);
                        mCurrentCharState = CharState.LPunchAir;
                    }
                }
            }

            #endregion

            //gravidade
            if (!isGrounded)
            {
                valorY -= gravity;
                if (valorY < fallingSpeedLimiter) valorY = fallingSpeedLimiter;
            }

            Velocity = (Vector2.UnitX * valorX) + (Vector2.UnitY * valorY);

            if (SpriteCurrentColumn == SpriteEndColumn)
            {
                isAttacking = false;
            }
            Collision();


            foreach(Keys k in movementKeyHistory)
            {
                //Console.WriteLine(k);
            }
            //Console.WriteLine("------------------------");
            movementKeyHistory.TrimExcess();

            Console.WriteLine(isGrounded);

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
                            SetSpriteAnimation(3, 0, 3, 5, 4);
                            break;
                        case CharState.WalkingRight:
                            this.SpriteEffects = SpriteEffects.None;
                            SetSpriteAnimation(3, 0, 3, 5, 4);
                            break;
                        case CharState.LPunch:
                            SetSpriteAnimation(4,0,4,3,3);
                            break;

                    }
                animationPlay = true;
                }
        }

        public void Collision()
        {
            foreach (var plataforma in mapa)
            {

                if (hurtbox.X +hurtbox.Width > plataforma.rect.X && hurtbox.X < plataforma.rect.X + plataforma.rect.Width)
                {
                    if (mPosition.Y <= plataforma.Position.Y + plataforma.Size.Y / 2 && mPosition.Y > plataforma.Position.Y - plataforma.Size.Y / 2)
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
            Game1.sSpriteBatch.Draw(a_text, hurtbox, Color.White);
            base.Draw();
        }

        public bool CheckHadouken()
        {
            if (movementKeyHistory.Dequeue() == down)
            {
                if (movementKeyHistory.Dequeue() == down)
                {
                    if (movementKeyHistory.Dequeue() == right)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;

        }
    }
}
