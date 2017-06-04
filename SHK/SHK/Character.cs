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
        private int airJumpCounter, comboCounter, airJumpDelay, comboDelay, invulFrames;
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
        public bool gotHit;

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
            attacks = new AttackList();
            listHadouken = new List<Projectil>();
            inimigoAttackList = new AttackList();
            hInimigo = new List<Projectil>();
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
            
            gotHit = false;
            Vector2 a = new Vector2(mPosition.X - mSize.X / 2 , mPosition.Y + mSize.Y / 2 - 40);
            //Vector2 a = new Vector2(mPosition.X, mPosition.Y);
            Vector2 b = new Vector2(mSize.X / 2 - 65, mSize.Y - mSize.Y / 3);
            hurtbox = Camera.ComputePixelRectangle(a, b);
            
            a_text = new Texture2D(Game1.mGraphics.GraphicsDevice, hurtbox.Width, hurtbox.Height);

            Color[] data = new Color[hurtbox.Width * hurtbox.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            a_text.SetData(data);

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

                        if (Keyboard.GetState().GetPressedKeys().Contains<Keys>(left) &&
                            Keyboard.GetState().GetPressedKeys().Contains<Keys>(right))
                        {
                            valorX = 0f;
                        }

                        /*if (Keyboard.GetState().IsKeyDown(down))
                        {
                            mCurrentCharState = CharState.Crouch;
                            isCrouched = true;
                            animationPlay = false;
                        }
                        if (Keyboard.GetState().IsKeyUp(down))
                        {
                            isCrouched = false;
                        }*/
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
                        isAttacking = true;
                        mCurrentCharState = CharState.LPunchAir;
                    }
                }
                animationPlay = false;
            }
            SpawnHitBox();

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
                hasFired = false;
                isAttacking = false;
                attacks.endAttack = true;
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
                    case CharState.Crouch:
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
                        SetSpriteAnimation(18, 0, 18, 3, 3);
                        break;
                    case CharState.cLPunch:
                        break;
                    case CharState.cLKick:
                        break;

                    //MEDIUM---------------------------------------------------------
                    case CharState.MKick:
                        SetSpriteAnimation(7, 0, 7, 6, 3);
                        break;
                    case CharState.MKickAir:
                        break;
                    case CharState.MPunch:
                        SetSpriteAnimation(5, 0, 5, 5, 3);
                        break;
                    case CharState.MPunchAir:
                        SetSpriteAnimation(19, 0, 19, 3, 3);
                        break;
                    case CharState.cMPunch:
                        break;
                    case CharState.cMKick:
                        break;

                    //HARD---------------------------------------------------------
                    case CharState.HKick:
                        SetSpriteAnimation(9, 0, 9, 8, 3);
                        break;
                    case CharState.HKickAir:
                        break;
                    case CharState.HPunch:
                        SetSpriteAnimation(6, 0, 6, 8, 3);
                        break;
                    case CharState.HPunchAir:
                        SetSpriteAnimation(24, 0, 24, 6, 3);
                        break;
                    case CharState.cHPunch:
                        break;
                    case CharState.cHKick:
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
                        invulFrames = 0;
                        playerHealth -= hadouken.damage;
                    }
                }
            }
            

        }

        public bool CheckHadouken()
        {
            if (movementKeyHistory.Dequeue() == down)
            {
                if (movementKeyHistory.Dequeue() == left || movementKeyHistory.Dequeue() == right)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public void Hadouken()
        {
            hasFired = true;
            Vector2 position = new Vector2(mPosition.X, mPosition.Y);
            Projectil aux = new Projectil(position, size, 1, 1, this.SpriteEffects);
            listHadouken.Add(aux);
            hadoukenPlay = true;
            

            /*public void Hadouken(Vector2 position, SpriteEffects spriteffects)
            {
                delay = Game1.gameTime.TotalGameTime;
                int pos = -95;
                if (spriteffects.Equals(SpriteEffects.FlipHorizontally))
                    pos = -205;
                damage = 5;
                endAttack = false;
                attackDuration = 1000;

                sizeHitbox = new Vector2(500, 500);
                positionHitbox = new Vector2((position.X + pos), (position.Y + 100f));
                SetDrawHitbox(positionHitbox, sizeHitbox, true);
            }*/

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
                        break;
                    case CharState.cLPunch:
                        break;
                    case CharState.cLKick:
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
                        break;
                    case CharState.cMPunch:
                        break;
                    case CharState.cMKick:
                        break;

                    //HARD---------------------------------------------------------
                    case CharState.HKick:
                        if (SpriteCurrentColumn == 3)
                            attacks.HeavyKick(mPosition, SpriteEffects);
                        else
                        {
                            attacks.hitbox.Location = new Point(-100, -100);
                        }
                        break;
                    case CharState.HKickAir:
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
                        break;
                    case CharState.cHPunch:
                        break;
                    case CharState.cHKick:
                        break;

                    //HARD---------------------------------------------------------
                    case CharState.Hadouken:
                        if (SpriteCurrentColumn == 6)
                            if(!hasFired)
                            { Hadouken(); }
   
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

           // Game1.sSpriteBatch.Draw(a_text, hurtbox, Color.White);
            base.Draw();
        }

       
    }
}
