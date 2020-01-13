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
    public class MovingObject : GameObject
    {
        public Vector2 Direction;
        public float Speed;

        public MovingObject(Vector2 Direction, float Speed, Vector2 Position, Texture2D Gfx, float Angle) : base(Position, Gfx, Angle)
        {
            this.Direction = Direction;
            this.Speed = Speed;
        }

        public virtual void Update(GameTime gameTime)
        {
            Position += Direction * Speed;
        }
    }
}
