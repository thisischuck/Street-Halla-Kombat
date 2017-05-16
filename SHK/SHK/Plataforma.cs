using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHK
{
    public class Plataforma : GameObject
    {
        bool isSoft;
        public Vector2 Size;
        public Vector2 Position;

        private Rectangle a;
        private Texture2D a_text;

        public Plataforma(bool isSoft, Vector2 size, Vector2 position) : base("game_plat", position, size)
        {
            this.isSoft = isSoft;
            this.Size = size;
            this.Position = position;

            a = Camera.ComputePixelRectangle(Position, Size);
            a_text = new Texture2D(Game1.mGraphics.GraphicsDevice, a.Width, a.Height);

            Color[] data = new Color[a.Width * a.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            a_text.SetData(data);
        }

        public override void Draw()
        {
            Game1.sSpriteBatch.Draw(a_text,a,Color.White);
            base.Draw();
        }
    }
}
