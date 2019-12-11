using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankNation
{
    class GameObject
    {
        public Vector2 pos
        {
            get;
            set;
        }
        public Texture2D gfx
        {
            get;
            set;
        }
        public float angle
        {
            get;
            set;
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 DrawOffset)
        {
            spriteBatch.Draw(gfx, pos - DrawOffset + new Vector2(400, 300), null,
                Color.White, angle + (float)Math.PI / 2, new Vector2(gfx.Width / 2, gfx.Height / 2), 1.0f
                SpriteEffects.None, 0);
        }
    }
}
