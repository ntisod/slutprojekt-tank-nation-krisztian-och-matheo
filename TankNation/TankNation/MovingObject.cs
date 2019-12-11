using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TankNation
{
    class MovingObject : GameObject
    {
        public Vector2 direction
        {
            get;
            set;
        }
        public float speed
        {
            get;
            set;
        }
        public virtual void Update(GameTime gameTime)
        {
            pos += direction * speed;
        }
    }
}
