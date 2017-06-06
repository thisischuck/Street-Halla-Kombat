using System;
using System.CodeDom;
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
        private float valorY, valorX, xSpeed, jumpSpeed, gravity, fallingSpeedLimiter;
        public bool isGrounded, hasAirJump, isAttacking, isRightDown, isLeftDown, isDownDown, hasHadouken, hasFired;
        private int airJumpCounter, comboCounter, airJumpDelay, comboDelay, invulFrames, stunnedFrames;
        private bool isAI, animationPlay, isCrouched;
        private int playerNumber;
        public int playerHealth;
        private Keys jump, right, left, down, lPunch, mPunch, hPunch, lKick, mKick, hKick;
        private List<Plataforma> mapa;
        public AttackList attacks;
        public Queue<Keys> movementKeyHistory;

        protected Rectangle hurtbox;
        private Texture2D a_text;

        private AttackList inimigoAttackList;
        private List<Projectil> hInimigo;

        public List<Projectil> listHadouken;

        private bool hadoukenPlay;
        public bool gotHit, stunned;
        public SpriteEffects effect;

        public Character(string imageName, Vector2 cposition, Vector2 csize, int row, int col, int padding, int player,
            SpriteEffects effect, bool ai, List<Plataforma> mapa, AttackList attacks) : base(imageName, cposition,
            csize, row, col, padding, effect)
        {
            mCurrentCharState = CharState.Idle;
            SetSpriteAnimation(0, 0, 0, 17, 2);
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
            isCrouched = false;
            comboCounter = 0;
            comboDelay = 50;
            invulFrames = 60;
            listHadouken = new List<Projectil>();
            this.effect = effect;
        }

        public Character(string imageName, Vector2 cposition, Vector2 csize, List<Plataforma> mapa) : base(imageName, cposition,
            csize, 18, 32, 0, SpriteEffects.None)
        {
            playerNumber = 1;
            mCurrentCharState = CharState.Idle;
            SetSpriteAnimation(0, 0, 0, 17, 2);
            position = cposition;
            size = csize;
            isGrounded = false;
            hasAirJump = true;
            isAttacking = false;
            valorX = 0f;
            valorY = 0f;
            xSpeed = 10f;
            jumpSpeed = 40f;
            gravity = 2f;
            fallingSpeedLimiter = -40f;
            animationPlay = false;
            airJumpCounter = 0;
            airJumpDelay = 12;
            SetKeys();
            this.mapa = mapa;
            movementKeyHistory = new Queue<Keys>();
            isRightDown = false;
            isLeftDown = false;
            isDownDown = false;
            hasHadouken = false;
            isCrouched = false;
            comboCounter = 0;
            comboDelay = 50;
            invulFrames = 60;
            attacks = new AttackList();
            listHadouken = new List<Projectil>();
            inimigoAttackList = new AttackList();
            hInimigo = new List<Projectil>();
            this.effect = SpriteEffects.None;
        }

        public void SetInimigo(AttackList inimigo, List<Projectil> hInimigo)
        {
            this.inimigoAttackList = inimigo;
            this.hInimigo = hInimigo;
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
            Hadouken,
            Crouch,
            Crouching,
            Falling,
            cLPunch,
            cMPunch,
            cHPunch,
            cLKick,
            cMKick,
            cHKick
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
            effect = SpriteEffects;   
            gotHit = false;

            if(isCrouched)
            {
                Vector2 a = new Vector2(mPosition.X - mSize.X / 2, mPosition.Y + mSize.Y / 2 - 100);
                Vector2 b = new Vector2(mSize.X / 2 - 65, mSize.Y / 2);
                hurtbox = Camera.ComputePixelRectangle(a, b);
            }
            else
            {
                Vector2 a = new Vector2(mPosition.X - mSize.X / 2, mPosition.Y + mSize.Y / 2 - 40);
                Vector2 b = new Vector2(mSize.X / 2 - 65, mSize.Y - mSize.Y / 3);
                hurtbox = Camera.ComputePixelRectangle(a, b);
            }

            /*a_text = new Texture2D(Game1.mGraphics.GraphicsDevice, hurtbox.Width, hurtbox.Height);

            Color[] data = new Color[hurtbox.Width * hurtbox.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            a_text.SetData(data);*/

            airJumpCounter++;
            if (hasHadouken)
                comboCounter++;

            if (comboCounter > comboDelay)
            {
                hasHadouken = false;
                comboCounter = 0;
            }
            if (movementKeyHistory.Count >= 3 && hasHadouken == false)
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

            if (!stunned)
            {
                if (!isAI)
                {
                    stunnedFrames = 0;
                    if (!isAttacking)
                    {
                        #region Movement + Animation


                        mPreviousCharState = mCurrentCharState;

                        if (!isGrounded)
                            isCrouched = false;
                        if (Keyboard.GetState().IsKeyDown(down))
                        {
                            mCurrentCharState = CharState.Crouching;
                            isCrouched = true;
                            animationPlay = false;
                        }
                        if (Keyboard.GetState().IsKeyUp(down))
                        {
                            isCrouched = false;
                        }

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

                            if (Keyboard.GetState().GetPressedKeys().Contains(left) &&
                                Keyboard.GetState().GetPressedKeys().Contains(right))
                            {
                                valorX = 0f;
                            }
                        }
                        else
                        {
                            valorX = 0f;
                        }

                        if (valorY == 0 && valorX == 0 && isGrounded && !isAttacking && !isCrouched)
                        {
                            mCurrentCharState = CharState.Idle;
                            animationPlay = false;
                        }

                        if (isGrounded && !isAttacking && !isCrouched)
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
                        if (!isCrouched)
                        {
                            if (Keyboard.GetState().IsKeyDown(lPunch))
                            {
                                valorX = 0;
                                isAttacking = true;
                                if (hasHadouken)
                                {

                                    mCurrentCharState = CharState.Hadouken;
                                }
                                else
                                {

                                    mCurrentCharState = CharState.LPunch;
                                }
                            }

                            if (Keyboard.GetState().IsKeyDown(mPunch))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.MPunch;
                            }

                            if (Keyboard.GetState().IsKeyDown(hPunch))
                            {
                                valorX = 0;
                                isAttacking = true;
                                if (hasHadouken)
                                {
                                    mCurrentCharState = CharState.Hadouken;
                                }
                                else
                                {
                                    mCurrentCharState = CharState.HPunch;
                                }
                            }

                            if (Keyboard.GetState().IsKeyDown(lKick))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.LKick;
                            }

                            if (Keyboard.GetState().IsKeyDown(mKick))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.MKick;
                            }

                            if (Keyboard.GetState().IsKeyDown(hKick))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.HKick;
                            }
                        }
                        else
                        {
                            if (Keyboard.GetState().IsKeyDown(lPunch))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.cLPunch;
                            }

                            if (Keyboard.GetState().IsKeyDown(mPunch))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.cMPunch;
                            }

                            if (Keyboard.GetState().IsKeyDown(hPunch))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.cHPunch;
                            }

                            if (Keyboard.GetState().IsKeyDown(lKick))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.cLKick;
                            }

                            if (Keyboard.GetState().IsKeyDown(mKick))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.cMKick;
                            }

                            if (Keyboard.GetState().IsKeyDown(hKick))
                            {
                                valorX = 0;
                                isAttacking = true;
                                mCurrentCharState = CharState.cHKick;
                            }
                        }
                    }
                    else
                    {
                        if (Keyboard.GetState().IsKeyDown(lPunch))
                        {
                            isAttacking = true;
                            mCurrentCharState = CharState.LPunchAir;
                        }

                        if (Keyboard.GetState().IsKeyDown(mPunch))
                        {
                            isAttacking = true;
                            mCurrentCharState = CharState.MPunchAir;
                        }

                        if (Keyboard.GetState().IsKeyDown(hPunch))
                        {
                            isAttacking = true;
                            mCurrentCharState = CharState.HPunchAir;
                        }

                        if (Keyboard.GetState().IsKeyDown(lKick))
                        {
                            isAttacking = true;
                            mCurrentCharState = CharState.LKickAir;
                        }

                        if (Keyboard.GetState().IsKeyDown(mKick))
                        {
                            isAttacking = true;
                            mCurrentCharState = CharState.MKickAir;
                        }

                        if (Keyboard.GetState().IsKeyDown(hKick))
                        {
                            isAttacking = true;
                            mCurrentCharState = CharState.HKickAir;
                        }
                    }
                    animationPlay = false;
                }
                SpawnHitBox();

                #endregion

                if (isCrouched)
                {
                    valorX = 0;
                }

            }
            else
            {
                valorX = 0;
                mCurrentCharState = CharState.Stunned;
                stunnedFrames++;
            }

            if (stunnedFrames > 20)
            {
                stunned = false;
            }
            //gravidade
            if (!isGrounded)
            {
                valorY -= gravity;
                if (valorY < fallingSpeedLimiter) valorY = fallingSpeedLimiter;
            }

            Velocity = (Vector2.UnitX * valorX) + (Vector2.UnitY * valorY);

            if (SpriteCurrentColumn == SpriteEndColumn)
            {
                hasFired = false;
                isAttacking = false;
                attacks.endAttack = true;
            }

            if (mCurrentCharState == CharState.Crouching)
            {
                if(SpriteCurrentColumn == SpriteEndColumn)
                    mCurrentCharState = CharState.Crouch;
            }

            if (mCurrentCharState == CharState.Air)
            {
                if (SpriteCurrentColumn == SpriteEndColumn)
                    mCurrentCharState = CharState.Falling;
            }
            

            CollisionMovement();
            if(invulFrames > 10)
                CollisionAttacks();

            invulFrames++;

            movementKeyHistory.TrimExcess();

            
            foreach (var hadouken in listHadouken)
            {
                hadouken.Update();
                if (hadouken.hitbox.X > Camera.CameraWindowUpperRightPosition.X || hadouken.hitbox.X < Camera.CameraWindowLowerLeftPosition.X)
                {
                    listHadouken.Remove(hadouken);
                    break;
                }
            }


            if (playerHealth < 0)
            {
                mCurrentCharState = CharState.Dead;
            }

            Console.WriteLine(isCrouched);
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
                    case CharState.Crouch:
                        SetSpriteAnimation(2, 2, 2, 2, 3);
                        break;

                    //LIGHTS---------------------------------------------------------
                    case CharState.LPunch:
                        SetSpriteAnimation(4, 0, 4, 3, 3);
                        break;
                    case CharState.LPunchAir:
                        SetSpriteAnimation(17, 0, 17, 3, 3);
                        break;
                    case CharState.LKick:
                        SetSpriteAnimation(8, 0, 8, 5, 3);
                        break;
                    case CharState.LKickAir:
                        SetSpriteAnimation(18, 0, 18, 2, 3);
                        break;
                    case CharState.cLPunch:
                        SetSpriteAnimation(11, 0, 11, 2, 4);
                        break;
                    case CharState.cLKick:
                        SetSpriteAnimation(14, 0, 14, 3, 3);
                        break;

                    //MEDIUM---------------------------------------------------------
                    case CharState.MKick:
                        SetSpriteAnimation(7, 0, 7, 6, 3);
                        break;
                    case CharState.MKickAir:
                        SetSpriteAnimation(20, 0, 20, 4, 3);
                        break;
                    case CharState.MPunch:
                        SetSpriteAnimation(5, 0, 5, 5, 3);
                        break;
                    case CharState.MPunchAir:
                        SetSpriteAnimation(19, 0, 19, 3, 3);
                        break;
                    case CharState.cMPunch:
                        SetSpriteAnimation(12, 0, 12, 2, 6);
                        break;
                    case CharState.cMKick:
                        SetSpriteAnimation(15, 0, 15, 6, 3);
                        break;

                    //HARD---------------------------------------------------------
                    case CharState.HKick:
                        SetSpriteAnimation(9, 0, 9, 8, 3);
                        break;
                    case CharState.HKickAir:
                        SetSpriteAnimation(21, 0, 21, 4, 3);
                        break;
                    case CharState.HPunch:
                        SetSpriteAnimation(6, 0, 6, 8, 3);
                        break;
                    case CharState.HPunchAir:
                        SetSpriteAnimation(24, 0, 24, 6, 3);
                        break;
                    case CharState.cHPunch:
                        SetSpriteAnimation(13, 0, 13, 8, 2);
                        break;
                    case CharState.cHKick:
                        SetSpriteAnimation(16, 0, 16, 7, 3);
                        break;

                    //Other---------------------------------------------------------
                    case CharState.Hadouken:
                        SetSpriteAnimation(26, 0, 26, 13, 3);
                        break;
                    case CharState.Dead:
                        SetSpriteAnimation(0, 0, 0, 0, 3);
                        break;
                    case CharState.Stunned:
                        SetSpriteAnimation(0, 0, 0, 0, 3);
                        break;
                    case CharState.Crouching:
                        SetSpriteAnimation(2, 0, 2, 2, 3);
                        break;
                    case CharState.Falling:
                        SetSpriteAnimation(1, 9, 1, 9, 2);
                        break;

                }
                animationPlay = true;
            }
        }

        public void CollisionMovement()
        {
            foreach (var plataforma in mapa)
            {

                if (hurtbox.X + hurtbox.Width > plataforma.rect.X &&
                    hurtbox.X < plataforma.rect.X + plataforma.rect.Width)
                {
                    if (hurtbox.Y <= plataforma.rect.Y + plataforma.rect.Height  &&
                        hurtbox.Y + hurtbox.Height >= plataforma.rect.Y)
                    {
                        isGrounded = true;
                        valorY = 0;
                        mPosition.Y = plataforma.Position.Y + plataforma.Size.Y/2 - 10;
                        break;
                    }
                }
                else
                {
                    isGrounded = false;
                }
            }

            if (hurtbox.X + 100 < Camera.CameraWindowLowerLeftPosition.X)
            {
                //mPosition.X = 2000;
            }
            else if (hurtbox.X > Camera.CameraWindowUpperRightPosition.X)
            {
                mPosition.X =  0 - mSize.X/2;
            }

        }

        public void CollisionAttacks()
        {
            if (inimigoAttackList.hitbox.X <= hurtbox.X + hurtbox.Width && inimigoAttackList.hitbox.X + inimigoAttackList.hitbox.Width >= hurtbox.X)
            {
                if (inimigoAttackList.hitbox.Y + inimigoAttackList.hitbox.Height >= hurtbox.Y && inimigoAttackList.hitbox.Y <= hurtbox.Y + hurtbox.Height)
                {
                    gotHit = true;
                    stunned = true;
                    invulFrames = 0;
                    playerHealth -= inimigoAttackList.damage;
                }
            }

            foreach (var hadouken in hInimigo)
            {   
                if (hadouken.hitbox.X <= hurtbox.X + hurtbox.Width && hadouken.hitbox.X + hadouken.hitbox.Width >= hurtbox.X)
                {
                    if (hadouken.hitbox.Y + hadouken.hitbox.Height >= hurtbox.Y && hadouken.hitbox.Y <= hurtbox.Y + hurtbox.Height)
                    {
                        gotHit = true;
                        stunned = true;
                        invulFrames = 0;
                        playerHealth -= hadouken.damage;
                        hInimigo.Remove(hadouken);
                        break;
                    }
                }
            }
            

        }

        public bool CheckHadouken()
        {
            if (movementKeyHistory.Dequeue() == down)
            {
                if (movementKeyHistory.Dequeue() == down)
                {
                    Keys a = movementKeyHistory.Dequeue();
                    if (a == left || a == right)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public void Hadouken()
        {
            int pos = 0;
            if (effect.Equals(SpriteEffects.FlipHorizontally))
                pos = -250;
            Console.WriteLine(effect);
            hasFired = true;
            Vector2 positionH = new Vector2(mPosition.X + pos, mPosition.Y+145);
            Vector2 sizeH = new Vector2(100,100);//\\
            string hadouken = "";
            if (playerNumber == 2)
            {
                hadouken = "PinkHadouken";
            }
            else if (playerNumber == 1)
            {
                hadouken = "BrazilianHadouken";
            }

            Projectil aux = new Projectil(hadouken, positionH, sizeH, 1, 1, this.SpriteEffects);
            listHadouken.Add(aux);
            hadoukenPlay = true;

        }

        public void SpawnHitBox()
        {
            if (isAttacking)
            {

                switch (mCurrentCharState)
                {
                    //LIGHTS---------------------------------------------------------
                    case CharState.LPunch:
                        if(SpriteCurrentColumn == 2)
                            attacks.LightPunch(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.LPunchAir:
                        if (SpriteCurrentColumn == 1)
                            attacks.LightPunchAir(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.LKick:
                        if (SpriteCurrentColumn == 3)
                            attacks.LightKick(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.LKickAir:
                        if (SpriteCurrentColumn == 1)
                            attacks.LightKickAir(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.cLPunch:
                        if (SpriteCurrentColumn == 1)
                            attacks.LightPunchCrouched(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.cLKick:
                        if (SpriteCurrentColumn == 1)
                            attacks.LightKickCrouched(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;

                    //MEDIUM---------------------------------------------------------
                    case CharState.MKick:
                        if (SpriteCurrentColumn == 3)
                            attacks.MediumKick(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.MKickAir:
                        if (SpriteCurrentColumn == 2)
                            attacks.MediumKickAir(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.MPunch:
                        if (SpriteCurrentColumn == 2)
                            attacks.MediumPunch(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.MPunchAir:
                        if (SpriteCurrentColumn == 2)
                            attacks.MediumPunchAir(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.cMPunch:
                        if (SpriteCurrentColumn == 1)
                            attacks.MediumPunchCrouched(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.cMKick:
                        if (SpriteCurrentColumn == 3)
                            attacks.MediumKickCrouched(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;

                    //HARD---------------------------------------------------------
                    case CharState.HKick:
                        if (SpriteCurrentColumn == 4)
                            attacks.HeavyKick(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.HKickAir:
                        if (SpriteCurrentColumn == 2)
                            attacks.HeavyKickAir(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.HPunch:
                        if (SpriteCurrentColumn == 3)
                            attacks.HeavyPunch(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.HPunchAir:
                        if (SpriteCurrentColumn == 3)
                            attacks.HeavyPunchAir(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.cHPunch:
                        if (SpriteCurrentColumn == 5)
                            attacks.HeavyPunchCrouched(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.cHKick:
                        if (SpriteCurrentColumn == 3)
                            attacks.HeavyKickCrouched(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;

                    //HARD---------------------------------------------------------
                    case CharState.Hadouken:
                       if (SpriteCurrentColumn == 6)
                            Hadouken();
                            /*if(!hasFired)
                            { Hadouken(); }*/
   
                        break;
                }
            }
        }

        public override void Draw()
        {
            AnimationUpdate();

            foreach (var hadouken in listHadouken)
            {
                hadouken.Draw();
            }

            //Game1.sSpriteBatch.Draw(a_text, hurtbox, Color.White);
            base.Draw();
        }

       
    }
}