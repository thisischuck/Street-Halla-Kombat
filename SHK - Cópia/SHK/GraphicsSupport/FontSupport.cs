using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    class FontSupport
    {
        static private SpriteFont sTheFont = null;
        static private Color sDefaultDrawColor = Color.White;
        static private Vector2 sStatusLocation = new Vector2(5, 5);

        public SpriteFont TheFont
        {
            get { return sTheFont; }
            set { sTheFont = value; }
        }

        public Color DefaultDrawColor
        {
            get { return sDefaultDrawColor; }
            set { sDefaultDrawColor = value; }
        }

        public Vector2 StatusLocation
        {
            get { return sStatusLocation; }
            set { sStatusLocation = value; }
        }

        static private void LoadFont()
        {
            // For demo purposes, loads Arial.spritefont
            if (null == sTheFont)
                sTheFont = Game1.sContent.Load<SpriteFont>("Arial");
        }

        static private Color ColorToUse(Nullable<Color> c)
        {
            return (null == c) ? sDefaultDrawColor : (Color)c;
        }

        static public void PrintStatus(String msg, Nullable<Color> drawColor)
        {
            LoadFont();
            Color useColor = ColorToUse(drawColor);
            // Compute top-left corner as the reference for output status
            Game1.sSpriteBatch.DrawString(sTheFont, msg, sStatusLocation, useColor);
        }

        static public void PrintStatusAt(Vector2 pos, String msg, Nullable<Color> drawColor)
        {
            LoadFont();
            Color useColor = ColorToUse(drawColor);
            int pixelX, pixelY;
            Camera.ComputePixelPosition(pos, out pixelX, out pixelY);
            Game1.sSpriteBatch.DrawString(sTheFont, msg,
           new Vector2(pixelX, pixelY), useColor);
        }
    }
}
