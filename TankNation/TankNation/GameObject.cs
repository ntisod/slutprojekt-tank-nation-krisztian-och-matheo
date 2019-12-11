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
        protected Texture2D gfx;
        protected Vector2 pos;

        public GameObject(Texture2D gfx, float x, float y)
        {
            this.gfx = gfx;
            this.pos = x;
            this.pos = y;
        }
    }
}
