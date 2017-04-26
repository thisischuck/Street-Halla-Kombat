using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    /// <summary>
    /// TexturedPrimitive class
    /// </summary>
    public partial class TexturedPrimitive
    {
        protected float mRotateAngle; // In radians, clockwise rotation
        // Support for drawing the image
        protected Texture2D mImage;     // The UWB-JPG.jpg image to be loaded
        public Vector2 mPosition;    // Center position of image        ***public porque na classe Game1 no método draw, não conseguia aceder a esta variável quando é protected***
        protected Vector2 mSize;        // Size of the image to be drawn
        public Vector2 MinBound { get { return mPosition - (0.5f * mSize); } } /// Accessors to the camera window bounds
        public Vector2 MaxBound { get { return mPosition + (0.5f * mSize); } } /// Accessors to the camera window bounds
        protected String mLabelString;  // String to draw
        protected Color mLabelColor = Color.Black;
        protected string ImageName;
        public float RotateAngleInRadian { get { return mRotateAngle; } set { mRotateAngle = value; } }
        public float Speed { get; internal set; }

        protected void InitPrimitive(String imageName, Vector2 position, Vector2 size, String label = null)
        {
            mImage = Game1.sContent.Load<Texture2D>(imageName);
            ImageName = imageName;
            mPosition = position;
            mSize = size;
            mRotateAngle = 0f;
            mLabelString = label;
            ReadColorData();
        }

        public TexturedPrimitive(String imageName, Vector2 position, Vector2 size, String label = null)
        {
            ImageName = imageName;
            InitPrimitive(imageName, position, size, label);
        }

        public TexturedPrimitive(String imageName, Vector2 position, Vector2 size)
        {
            ImageName = imageName;
            mImage = Game1.sContent.Load<Texture2D>(imageName);
            mPosition = position;
            mSize = size;
            mRotateAngle = 0f;
            ReadColorData();
        }

        public TexturedPrimitive(String imageName)  
        {
            ImageName = imageName;
            mImage = Game1.sContent.Load<Texture2D>(imageName);
            mRotateAngle = 0f;
            ReadColorData();
        }

        public void Update(Vector2 deltaTranslate, Vector2 deltaScale)
        {
            mPosition += deltaTranslate;
            mSize += deltaScale;
        }

        public void Update(Vector2 deltaTranslate, Vector2 deltaScale, float deltaAngleInRadian)
        {
            mPosition += deltaTranslate;
            mSize += deltaScale;
            mRotateAngle += deltaAngleInRadian;
        }

        virtual public void Draw()
        {
            // Defines where and size of the texture to show
            Rectangle destRect = Camera.ComputePixelRectangle(mPosition, mSize);
            Game1.sSpriteBatch.Draw(mImage, destRect, Color.White);
        }


        public bool PrimitivesTouches(TexturedPrimitive otherPrim)      /// nao é preciso
        {
            Vector2 v = mPosition - otherPrim.mPosition;
            float dist = v.Length();
            return (dist < ((mSize.X / 2f) + (otherPrim.mSize.X / 2f)));
        }


        protected virtual int SpriteTopPixel { get { return 0; } }
        protected virtual int SpriteLeftPixel { get { return 0; } }
        protected virtual int SpriteImageWidth { get { return mImage.Width; } }
        protected virtual int SpriteImageHeight { get { return mImage.Height; } }
    }
}
