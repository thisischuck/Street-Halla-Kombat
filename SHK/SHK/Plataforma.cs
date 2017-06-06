using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    public class Plataforma
    {
        bool isSoft;
        public Vector2 Size;
        public Vector2 Position;

        public Rectangle rect;
        private Texture2D a_text;

        public Plataforma(bool isSoft, Vector2 size, Vector2 position)
        {
            this.isSoft = isSoft;
            this.Size = size;
            this.Position = position;

            rect = Camera.ComputePixelRectangle(Position, Size);
            a_text = new Texture2D(Game1.mGraphics.GraphicsDevice, rect.Width, rect.Height);

            Color[] data = new Color[rect.Width * rect.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.DeepPink;
            a_text.SetData(data);
        }

        public void Draw()
        {
           Game1.sSpriteBatch.Draw(a_text,rect,Color.White);
        }
    }
}
