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
    public class Meter : GameObject
    {
        public float Value;

        public Meter(Vector2 Position, Texture2D Gfx, float Angle) : base(Position, Gfx, Angle)
        {
            Value = 0;
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 DrawOffset)
        {

            spriteBatch.Draw(Gfx, new Rectangle((int)base.Position.X,
                (int)base.Position.Y,
                (int)this.Value, Gfx.Height), Color.White);
        }

    }
}
