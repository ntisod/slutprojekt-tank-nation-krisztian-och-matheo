using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TankNation
{
    class Shot : MovingObject
    {

        public float Power;

        public Shot(float Power, Vector2 Direction, float Speed, Vector2 Position, Texture2D Gfx, float Angle) : base(Direction, Speed, Position, Gfx, Angle)
        {
            this.Power = Power;
        }

        public override void Update(GameTime gameTime)
        {     

            Power -= 1.1F;
            base.Update(gameTime);
        }

    }
}
